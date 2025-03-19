using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MNMovies.Models;
using Newtonsoft.Json;

namespace MNMovies.Controllers.AdminControllers
{
    public class EpisodeController : Controller
    {

        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public EpisodeController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region EpisodeList
        [HttpGet]
        [Route("/EpisodeList")]
        public IActionResult EpisodeList()
        {
            AddAuthorizationHeader();

            List<EpisodeModel> episodeList = new List<EpisodeModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Episode").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                episodeList = JsonConvert.DeserializeObject<List<EpisodeModel>>(data);
            }
            return View("EpisodeList", episodeList);
        }
        #endregion

        #region SeasonWiseEpisodeList
        [HttpGet]
        [Route("/SeasonWiseEpisodeList")]
        public IActionResult SeasonWiseEpisodeList(int SeasonID)
        {
            AddAuthorizationHeader();

            List<EpisodeModel> seasonWiseEpisodeList = new List<EpisodeModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Episode/Season/{SeasonID}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                seasonWiseEpisodeList = JsonConvert.DeserializeObject<List<EpisodeModel>>(data);
            }
            return View("SeasonWiseEpisodeList", seasonWiseEpisodeList);
        }
        #endregion

        #region EpisodeAddEdit
        [HttpGet]
        [Route("/Episode/EpisodeAddEdit")]
        public IActionResult EpisodeAddEdit(int? EpisodeID,int? SeasonID,int? SeriesID)
        {
            AddAuthorizationHeader();

            EpisodeModel model = new EpisodeModel();

            if (EpisodeID.HasValue && EpisodeID > 0)
            {
                HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Episode/{EpisodeID}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<EpisodeModel>(data);
                }
                ViewBag.ExistingImage = model.EpisodeImage;
                ViewBag.ExistingVideo = model.EpisodeVideo;
            }

            if (SeasonID.HasValue && SeasonID > 0)
            {
                HttpResponseMessage seasonResponse = _Client.GetAsync($"{_Client.BaseAddress}/Season/{SeasonID}").Result;
                if (seasonResponse.IsSuccessStatusCode)
                {
                    string seasonData = seasonResponse.Content.ReadAsStringAsync().Result;
                    var season = JsonConvert.DeserializeObject<SeasonModel>(seasonData);
                    ViewBag.SeasonNumber = season.SeasonNumber;
                    ViewBag.SeasonID = season.SeasonID;
                }
            }

            if (SeriesID.HasValue && SeriesID > 0)
            {
                HttpResponseMessage seriesResponse = _Client.GetAsync($"{_Client.BaseAddress}/Series/{SeriesID}").Result;
                if (seriesResponse.IsSuccessStatusCode)
                {
                    string seriesData = seriesResponse.Content.ReadAsStringAsync().Result;
                    var series = JsonConvert.DeserializeObject<SeriesModel>(seriesData);
                    ViewBag.SeriesName = series.SeriesName;
                    ViewBag.SeriesID = series.SeriesID;
                }
            }

            return View("EpisodeAddEdit", model);
        }
        #endregion

        #region EpisodeSave
        [HttpPost]
        [Route("/Episode/EpisodeSave")]
        public IActionResult EpisodeSave(EpisodeModel model)
        {
            if (ModelState.IsValid)
            {
                AddAuthorizationHeader();

                HttpResponseMessage response;
                if (model.EpisodeID == null)
                {
                    // Add operation
                    response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/Episode", model).Result;
                }
                else
                {
                    // Edit operation
                    response = _Client.PutAsJsonAsync($"{_Client.BaseAddress}/Episode/{model.EpisodeID}", model).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("EpisodeList");
                }
                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Failed to save the Episode. Error: { errorMessage}";
            }

            // Return to the form if validation or API call fails
            return View("EpisodeAddEdit", model);
        }
        #endregion

        #region EpisodeDelete
        [HttpPost]
        public IActionResult EpisodeDelete(int EpisodeID)
        {
            AddAuthorizationHeader();

            HttpResponseMessage response = _Client.DeleteAsync($"{_Client.BaseAddress}/Episode/{EpisodeID}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("EpisodeList");
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Error deleting the Episode. Error : {errorMessage}";
                return RedirectToAction("EpisodeList");
            }

        }
        #endregion

        #region AddAuthorizationHeader
        private void AddAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("JWTToken");
            if (!string.IsNullOrEmpty(token))
            {
                _Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
        #endregion

    }
}
