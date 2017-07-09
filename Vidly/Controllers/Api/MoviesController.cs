using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
        }

        // GET /api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            return this.context.Movies
                .Include("Genre")
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = this.context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = Mapper.Map<Movie>(movieDto);
            this.context.Movies.Add(movie);
            this.context.SaveChanges();

            movieDto.Id = movie.Id;
            return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
        }

        //PUT /api/movies
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movieToEdit = this.context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieToEdit == null)
            {
                return NotFound();
            }

            movieDto.Id = movieToEdit.Id;
            Mapper.Map(movieDto, movieToEdit);
            this.context.SaveChanges();

            return Ok(movieDto);
        }

        // DELETE /api/movies
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieToDelete = this.context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieToDelete == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            this.context.Movies.Remove(movieToDelete);
            this.context.SaveChanges();
        }
    }
}
