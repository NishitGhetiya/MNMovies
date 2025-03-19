using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MNMovies_Backend.Data;
using MNMovies_Backend.Models;

namespace MNMovies_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly MovieRepository _movieRepository;

        public MovieController(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = _movieRepository.SelectAll();
            return Ok(movies);
        }

        [HttpGet]
        [Route("TopTen")]
        public IActionResult GetTopTen()
        {
            var movies = _movieRepository.SelectTopTen();
            return Ok(movies);
        }

        [HttpGet]
        [Route("Latest")]
        public IActionResult GetLatest()
        {
            var movies = _movieRepository.SelectLatest();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieByID(int id)
        {
            var movie = _movieRepository.SelectByPK(id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult InsertMovie([FromBody] MovieModel movieModel)
        {
            if (movieModel == null)
            {
                return BadRequest();
            }
            bool IsInserted = _movieRepository.Insert(movieModel);
            if (IsInserted)
            {
                return Ok(new { message = "Movie Insterted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Instert The Movie");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody] MovieModel movieModel)
        {
            if (movieModel == null || id != movieModel.MovieID)
            {
                return BadRequest();
            }
            var IsUpdated = _movieRepository.Update(movieModel);
            if (!IsUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var isDelete = _movieRepository.Delete(id);
            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("IncrementViews/{id}")]
        public IActionResult IncrementViews(int id)
        {
            var isUpdated = _movieRepository.IncrementViews(id);
            if (!isUpdated)
            {
                return NotFound(new { message = "Movie not found or failed to update views" });
            }
            return Ok(new { message = "Views incremented successfully" });
        }

        [HttpGet]
        [Route("TopForHomeSlider")]
        public IActionResult GetTopForHomeSlider()
        {
            var movies = _movieRepository.SelectTopForHomeSlider();
            return Ok(movies);
        }

        [HttpGet]
        [Route("LatestForHomeSlider")]
        public IActionResult GetLatestForHomeSlider()
        {
            var movies = _movieRepository.SelectLatestForHomeSlider();
            return Ok(movies);
        }

        [HttpGet]
        [Route("Count")]
        public IActionResult MovieCount()
        {
            int count = _movieRepository.Count();
            return Ok(count);
        }

    }
}
