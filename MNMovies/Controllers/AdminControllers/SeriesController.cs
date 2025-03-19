using Microsoft.AspNetCore.Mvc;
using MNMovies.Models;
using Newtonsoft.Json;

namespace MNMovies.Controllers.AdminControllers
{
    [CheckAccess]
    public class SeriesController : Controller
    {

        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public SeriesController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region SeriesList
        [HttpGet]
        [Route("/SeriesList")]
        public IActionResult SeriesList()
        {
            AddAuthorizationHeader();

            List<SeriesModel> SeriesList = new List<SeriesModel>();

            try
            {
                HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Series").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    SeriesList = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
                }
                else
                {
                    TempData["ErrorMassage"] = "Failed to retrieve series list from the server.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMassage"] = $"An unexpected error occurred while fetching the series list. Error: {ex.Message}";
            }

            return View("SeriesList", SeriesList);
        }
        #endregion

        #region SeriesAddEdit
        [HttpGet]
        [Route("/Series/SeriesAddEdit")]
        public IActionResult SeriesAddEdit(int? SeriesID)
        {
            AddAuthorizationHeader();

            SeriesModel model = new SeriesModel();

            if (SeriesID.HasValue && SeriesID > 0)
            {
                HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Series/{SeriesID}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<SeriesModel>(data);
                }
                ViewBag.ExistingImage = model.SeriesImage;
            }

            return View("SeriesAddEdit", model);
        }
        #endregion

        #region SeriesSave
        [HttpPost]
        [Route("/Series/SeriesSave")]
        public IActionResult SeriesSave(SeriesModel model)
        {
            if (ModelState.IsValid)
            {
                AddAuthorizationHeader();

                HttpResponseMessage response;
                if (model.SeriesID == null)
                {
                    // Add operation
                    response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/Series", model).Result;
                }
                else
                {
                    // Edit operation
                    response = _Client.PutAsJsonAsync($"{_Client.BaseAddress}/Series/{model.SeriesID}", model).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SeriesList");
                }

                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Failed to save the Series. Error: {errorMessage}";
            }

            // Return to the form if validation or API call fails
            return View("SeriesAddEdit", model);
        }
        #endregion

        #region SeriesDelete
        [HttpPost]
        public IActionResult SeriesDelete(int SeriesID)
        {
            try
            {
                AddAuthorizationHeader();

                HttpResponseMessage response = _Client.DeleteAsync($"{_Client.BaseAddress}/Series/{SeriesID}").Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("SeriesList");
                }
                else
                {
                    var errorMessage = response.Content.ReadAsStringAsync().Result;
                    TempData["ErrorMassage"] = $"Error deleting the Series. Error: {errorMessage}";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMassage"] = $"An unexpected error occurred while deleting the series. Error: {ex.Message}";
            }

            return RedirectToAction("SeriesList");
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
