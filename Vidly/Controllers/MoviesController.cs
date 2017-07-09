using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext context;

        public MoviesController()
        {
            this.context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            this.context.Dispose();
        }

        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var model = new MovieFormViewModel
            {
                Movie = new Movie(),
                GenreList = this.context.Genres.ToList(),
            };

            return View("MovieForm", model);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var model = new MovieFormViewModel
                {
                    Movie = movie,
                    GenreList = this.context.Genres.ToList()
                };

                return View("MovieForm", model);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                this.context.Movies.Add(movie);
            }
            else
            {
                var movieToEdit = this.context.Movies.Single(m => m.Id == movie.Id);
                movieToEdit.Name = movie.Name;
                movieToEdit.ReleaseDate = movie.ReleaseDate;
                movieToEdit.Genre = movie.Genre;
                movieToEdit.NumberInStock = movie.NumberInStock;
            }

            this.context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = this.context.Movies.Include("Genre").SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var model = new MovieFormViewModel
            {
                Movie = movie,
                GenreList = this.context.Genres.ToList()
            };
            return View("MovieForm", model);
        }
    }
}