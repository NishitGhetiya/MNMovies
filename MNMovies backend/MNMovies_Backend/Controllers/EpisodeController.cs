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
    public class EpisodeController : ControllerBase
    {
        private readonly EpisodeRepository _episodeRepository;

        public EpisodeController(EpisodeRepository episodeRepository)
        {
            _episodeRepository = episodeRepository;
        }

        [HttpGet]
        public IActionResult GetAllEpisodes()
        {
            var episodes = _episodeRepository.SelectAll();
            return Ok(episodes);
        }

        [HttpGet("{id}")]
        public IActionResult GetEpisodeByID(int id)
        {
            var episode = _episodeRepository.SelectByPK(id);
            if (episode == null)
            {
                return NotFound();
            }
            return Ok(episode);
        }

        [HttpGet("Season/{id}")]
        public IActionResult GetEpisodeBySeasonID(int id)
        {
            var episode = _episodeRepository.SelectBySeasonID(id);
            if (episode == null)
            {
                return NotFound();
            }
            return Ok(episode);
        }

        [HttpPost]
        public IActionResult InsertEpisode([FromBody] EpisodeModel episodeModel)
        {
            if (episodeModel == null)
            {
                return BadRequest();
            }
            bool IsInserted = _episodeRepository.Insert(episodeModel);
            if (IsInserted)
            {
                return Ok(new { message = "Episode Insterted Successfully" });
            }
            return StatusCode(500, "An Error Occurred while Instert The Episode");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEpisode(int id, [FromBody] EpisodeModel episodeModel)
        {
            if (episodeModel == null || id != episodeModel.EpisodeID)
            {
                return BadRequest();
            }
            var IsUpdated = _episodeRepository.Update(episodeModel);
            if (!IsUpdated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id}")]
        public IActionResult DeleteEpisode(int id)
        {
            var isDelete = _episodeRepository.Delete(id);
            if (!isDelete)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
