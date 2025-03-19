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
    public class SeasonController : ControllerBase
    {
        private readonly SeasonRepository _seasonRepository;

        public SeasonController(SeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        [HttpGet]
        public IActionResult GetAllSeasons()
        {
            var seasons = _seasonRepository.SelectAll();
            return Ok(seasons);
        }

        [HttpGet("{id}")]
        public IActionResult GetSeasonByID(int id)
        {
            var season = _seasonRepository.SelectByPK(id);
            if (season == null)
            {
                return NotFound();
            }
            return Ok(season);
        }

        [HttpGet("Series/{id}")]
        public IActionResult GetSeasonBySeriesID(int id)
        {
            var season = _seasonRepository.SelectBySeriesID(id);
            if (season == null)
            {
                return NotFound();
            }
            return Ok(season);
        }

        [HttpPost]
        public IActionResult InsertSeason([FromBody] SeasonModel seasonModel)
        {
            if (seasonModel == null)
            {
                return BadRequest();
            }
            bool IsInserted = _seasonRepository.Insert(seasonModel);
            if (IsInserted)
            {
                return Ok(new { message = "Season Insterted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Instert The Season");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSeason(int id, [FromBody] SeasonModel seasonModel)
        {
            if (seasonModel == null || id != seasonModel.SeasonID)
            {
                return BadRequest();
            }
            var IsUpdated = _seasonRepository.Update(seasonModel);
            if (!IsUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteSeason(int id)
        {
            var isDelete = _seasonRepository.Delete(id);
            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize]
        [HttpGet("Count")]
        public IActionResult SeasonCount()
        {
            var seasonCounts = _seasonRepository.Count();
            if (seasonCounts == null || !seasonCounts.Any())
            {
                return NotFound("No season counts available.");
            }
            return Ok(seasonCounts);
        }

    }
}
