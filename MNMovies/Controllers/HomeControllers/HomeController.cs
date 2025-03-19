using Microsoft.AspNetCore.Mvc;
using MNMovies.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MNMovies.Controllers.HomeControllers
{
    [CheckAccess]
    public class HomeController : Controller
    {
        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public HomeController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region Index
        [Route("/")]
        public IActionResult Index()
        {
            AddAuthorizationHeader();

            List<MovieModel> LatestMoviesList = new List<MovieModel>();
            HttpResponseMessage movieresponse = _Client.GetAsync($"{_Client.BaseAddress}/Movie/Latest").Result;
            if (movieresponse.IsSuccessStatusCode)
            {
                string data = movieresponse.Content.ReadAsStringAsync().Result;

                LatestMoviesList = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            List<SeriesModel> LatestSeriesList = new List<SeriesModel>();
            HttpResponseMessage seriesresponse = _Client.GetAsync($"{_Client.BaseAddress}/Series/Latest").Result;
            if (seriesresponse.IsSuccessStatusCode)
            {
                string data = seriesresponse.Content.ReadAsStringAsync().Result;

                LatestSeriesList = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }

            List<MovieModel> LatestMoviesListForSlider = new List<MovieModel>();
            HttpResponseMessage response1 = _Client.GetAsync($"{_Client.BaseAddress}/Movie/LatestForHomeSlider").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;

                LatestMoviesListForSlider = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            List<MovieModel> TopMoviesListForSlider = new List<MovieModel>();
            HttpResponseMessage response2 = _Client.GetAsync($"{_Client.BaseAddress}/Movie/TopForHomeSlider").Result;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;

                TopMoviesListForSlider = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            List<SeriesModel> LatestSeriesListForSlider = new List<SeriesModel>();
            HttpResponseMessage response3 = _Client.GetAsync($"{_Client.BaseAddress}/Series/LatestForHomeSlider").Result;
            if (response3.IsSuccessStatusCode)
            {
                string data = response3.Content.ReadAsStringAsync().Result;

                LatestSeriesListForSlider = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }

            List<SeriesModel> TopSeriesListForSlider = new List<SeriesModel>();
            HttpResponseMessage response4 = _Client.GetAsync($"{_Client.BaseAddress}/Series/TopForHomeSlider").Result;
            if (response4.IsSuccessStatusCode)
            {
                string data = response4.Content.ReadAsStringAsync().Result;

                TopSeriesListForSlider = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }


            ViewBag.LatestMovies = LatestMoviesList;
            ViewBag.LatestSeries = LatestSeriesList;

            ViewBag.LatestMoviesForSlider = LatestMoviesListForSlider;
            ViewBag.TopMoviesForSlider = TopMoviesListForSlider;

            ViewBag.LatestSeriesForSlider = LatestSeriesListForSlider;
            ViewBag.TopSeriesForSlider = TopSeriesListForSlider;

            return View("Index");
        }
        #endregion

        #region Privacy
        public IActionResult Privacy()
        {
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
