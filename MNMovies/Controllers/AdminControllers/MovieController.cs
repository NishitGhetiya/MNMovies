using Microsoft.AspNetCore.Mvc;
using MNMovies.Models;
using Newtonsoft.Json;

namespace MNMovies.Controllers.AdminControllers
{
    [CheckAccess]
    public class MovieController : Controller
    {

        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public MovieController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region MovieList
        [HttpGet]
        [Route("/MovieList")]
        public IActionResult MovieList()
        {
            AddAuthorizationHeader();

            List<MovieModel> movieList = new List<MovieModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Movie").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                movieList = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }
            return View("MovieList", movieList);
        }
        #endregion

        #region MovieAddEdit
        [HttpGet]
        [Route("/Movie/MovieAddEdit")]
        public IActionResult MovieAddEdit(int? MovieID)
        {
            AddAuthorizationHeader();

            MovieModel model = new MovieModel();

            if (MovieID.HasValue && MovieID > 0)
            {
                HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Movie/{MovieID}").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<MovieModel>(data);
                }
                ViewBag.ExistingImage = model.MovieImage;
                ViewBag.ExistingVideo = model.MovieVideo;
            }

            return View("MovieAddEdit", model);
        }
        #endregion

        #region MovieSave
        [HttpPost]
        public IActionResult MovieSave(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                AddAuthorizationHeader();

                HttpResponseMessage response;
                if (model.MovieID == null)
                {
                    // Add operation
                    response = _Client.PostAsJsonAsync($"{_Client.BaseAddress}/Movie", model).Result;
                }
                else
                {
                    // Edit operation
                    response = _Client.PutAsJsonAsync($"{_Client.BaseAddress}/Movie/{model.MovieID}", model).Result;
                }

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("MovieList");
                }

                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Failed to save the Movie. Error: {errorMessage}";
            }

            // Return to the form if validation or API call fails
            return View("MovieAddEdit", model);
        }
        #endregion

        #region MovieDelete
        [HttpPost]
        public IActionResult MovieDelete(int MovieID)
        {
            AddAuthorizationHeader();

            HttpResponseMessage response = _Client.DeleteAsync($"{_Client.BaseAddress}/Movie/{MovieID}").Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("MovieList");
            }
            else
            {
                var errorMessage = response.Content.ReadAsStringAsync().Result;
                TempData["ErrorMassage"] = $"Error deleting the Movie. Error : {errorMessage}";
                return RedirectToAction("MovieList");
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
