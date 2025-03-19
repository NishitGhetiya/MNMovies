using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MNMovies.Models;
using Newtonsoft.Json;

namespace MNMovies.Controllers.AdminControllers
{
    [CheckAccess]
    public class SeasonController : Controller
    {

        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public SeasonController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region SeasonList
        [HttpGet]
        [Route("/SeasonList")]
        public IActionResult SeasonList()
        {
            AddAuthorizationHeader();

            List<SeasonModel> seasonList = new List<SeasonModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Season").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                seasonList = JsonConvert.DeserializeObject<List<SeasonModel>>(data);
            }
            return View("SeasonList", seasonList);
        }
        #endregion

        #region SeriesWiseSeasonList
        [HttpGet]
        [Route("/SeriesWiseSeasonList")]
        public IActionResult SeriesWiseSeasonList(int SeriesID)
        {
            AddAuthorizationHeader();

            List<SeasonModel> seriesWiseSeasonList = new List<SeasonModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Season/Series/{SeriesID}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                seriesWiseSeasonList = JsonConvert.DeserializeObject<List<SeasonModel>>(data);
            }
            return View("SeriesWiseSeasonList", seriesWiseSeasonList);
        }
        #endregion

        #region SeasonAddEdit
        [HttpGet]
        [Route("/Season/SeasonAddEdit")]
        public IActionResult SeasonAddEdit(int? SeasonID,int? SeriesID)
        {
            AddAuthorizationHeader();

            SeasonModel model = new SeasonModel();

            if (SeasonID.HasValue && SeasonID > 0)
            {
                HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Season/{SeasonID}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<SeasonModel>(data);
                }
                ViewBag.ExistingImage = model.SeasonImage;
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
            
            return View("SeasonAddEdit", model);
        }
        #endregion

        #region SeasonSave
        [HttpPost]
        [Route("/Season/SeasonSave")]
        public IActionResult SeasonSave(SeasonModel model)
        {
            if (ModelState.IsValid)
            {
                AddAuthorizationHeader();

                HttpResponseMessage response;
                if (model.SeasonID == null)
                {
                    // Add operation
                    response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/Season", model).Result;
                }
                else
                {
                    // Edit operation
                    response = _Client.PutAsJsonAsync($"{_Client.BaseAddress}/Season/{model.SeasonID}", model).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SeasonList");
                }

                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Failed to save the Season. Error: {errorMessage}";
            }

            // Return to the form if validation or API call fails
            return View("SeasonAddEdit", model);
        }
        #endregion

        #region SeasonDelete
        [HttpPost]
        public IActionResult SeasonDelete(int SeasonID)
        {
            try
            {
                AddAuthorizationHeader();

                HttpResponseMessage response = _Client.DeleteAsync($"{_Client.BaseAddress}/Season/{SeasonID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SeasonList");
                }
                else
                {
                    var errorMessage = response.Content.ReadAsStringAsync().Result;
                    TempData["ErrorMassage"] = $"Error deleting the Season. Error: {errorMessage}";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMassage"] = $"An unexpected error occurred: {ex.Message}";
            }

            return RedirectToAction("SeasonList");
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
