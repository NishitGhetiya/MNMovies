using Microsoft.AspNetCore.Mvc;
using MNMovies.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MNMovies.Controllers.AdminControllers
{
    [CheckAccess]
    public class AdminController : Controller
    {

        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public AdminController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            AddAuthorizationHeader();

            int movieCount = 0;
            HttpResponseMessage response = await _Client.GetAsync($"{_Client.BaseAddress}/Movie/Count");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                movieCount = JsonConvert.DeserializeObject<int>(jsonResponse);
            }

            int seriesCount = 0;
            HttpResponseMessage response1 = await _Client.GetAsync($"{_Client.BaseAddress}/Series/Count");

            if (response1.IsSuccessStatusCode)
            {
                var jsonResponse1 = await response1.Content.ReadAsStringAsync();
                seriesCount = JsonConvert.DeserializeObject<int>(jsonResponse1);
            }

            List<MovieModel> topTenMoviesList = new List<MovieModel>();
            HttpResponseMessage response2 = _Client.GetAsync($"{_Client.BaseAddress}/Movie/TopTen").Result;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;

                topTenMoviesList = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            List<SeriesModel> topTenSeriesList = new List<SeriesModel>();
            HttpResponseMessage response3 = _Client.GetAsync($"{_Client.BaseAddress}/Series/TopTen").Result;
            if (response3.IsSuccessStatusCode)
            {
                string data = response3.Content.ReadAsStringAsync().Result;

                topTenSeriesList = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }

            List<SeasonCountModel> seasonCountsList = new List<SeasonCountModel>();
            HttpResponseMessage response4 = await _Client.GetAsync($"{_Client.BaseAddress}/Season/Count");
            if (response4.IsSuccessStatusCode)
            {
                string seasonCountData = await response4.Content.ReadAsStringAsync();
                seasonCountsList = JsonConvert.DeserializeObject<List<SeasonCountModel>>(seasonCountData);
            }

            ViewBag.MovieCount = movieCount;
            ViewBag.SeriesCount = seriesCount;

            ViewBag.TopTenMovies = topTenMoviesList;
            ViewBag.TopTenSeries = topTenSeriesList;

            ViewBag.SeasonCounts = seasonCountsList;

            return View();
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
