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
    public class SeriesController : ControllerBase
    {
        private readonly SeriesRepository _seriesRepository;

        public SeriesController(SeriesRepository seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        [HttpGet]
        public IActionResult GetAllSeries()
        {
            var series = _seriesRepository.SelectAll();
            return Ok(series);
        }

        [HttpGet]
        [Route("TopTen")]
        public IActionResult GetTopTen()
        {
            var series = _seriesRepository.SelectTopTen();
            return Ok(series);
        }

        [HttpGet]
        [Route("Latest")]
        public IActionResult GetLatest()
        {
            var series = _seriesRepository.SelectLatest();
            return Ok(series);
        }

        [HttpGet("{id}")]
        public IActionResult GetSeriesByID(int id)
        {
            var series = _seriesRepository.SelectByPK(id);
            if (series == null)
            {
                return NotFound();
            }
            return Ok(series);
        }

        [HttpPost]
        public IActionResult InsertSeries([FromBody] SeriesModel seriesModel)
        {
            if (seriesModel == null)
            {
                return BadRequest();
            }
            bool IsInserted = _seriesRepository.Insert(seriesModel);
            if (IsInserted)
            {
                return Ok(new { message = "Series Insterted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Instert The Series");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSeries(int id, [FromBody] SeriesModel seriesModel)
        {
            if (seriesModel == null || id != seriesModel.SeriesID)
            {
                return BadRequest();
            }
            var IsUpdated = _seriesRepository.Update(seriesModel);
            if (!IsUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteSeries(int id)
        {
            var isDelete = _seriesRepository.Delete(id);
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
            var isUpdated = _seriesRepository.IncrementViews(id);
            if (!isUpdated)
            {
                return NotFound(new { message = "Series not found or failed to update views" });
            }
            return Ok(new { message = "Views incremented successfully" });
        }

        [HttpGet]
        [Route("LatestForHomeSlider")]
        public IActionResult GetLatestForHomeSlider()
        {
            var series = _seriesRepository.SelectLatestForHomeSlider();
            return Ok(series);
        }

        [HttpGet]
        [Route("TopForHomeSlider")]
        public IActionResult GetTopForHomeSlider()
        {
            var series = _seriesRepository.SelectTopForHomeSlider();
            return Ok(series);
        }

        [HttpGet]
        [Route("Count")]
        public IActionResult SeriesCount()
        {
            int count = _seriesRepository.Count();
            return Ok(count);
        }
    }
}
