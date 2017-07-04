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
            var movies = this.context.Movies.Include("Genre").ToList();
            return View(movies);
        }

        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            var movie = this.context.Movies.Include("Genre").FirstOrDefault(m => m.Id == id);
            return View(movie);
        }
    }
}