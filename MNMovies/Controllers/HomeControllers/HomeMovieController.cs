using Microsoft.AspNetCore.Mvc;
using MNMovies.Models;
using Newtonsoft.Json;

namespace MNMovies.Controllers.HomeControllers
{
    [CheckAccess]
    public class HomeMovieController : Controller
    {
        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public HomeMovieController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region Movies
        [HttpGet]
        [Route("/Movies")]
        public IActionResult Movies()
        {
            AddAuthorizationHeader();

            List<MovieModel> movieList = new List<MovieModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Movie").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                movieList = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            List<MovieModel> topTenMoviesList = new List<MovieModel>();
            HttpResponseMessage response1 = _Client.GetAsync($"{_Client.BaseAddress}/Movie/TopTen").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;

                topTenMoviesList = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            List<MovieModel> LatestMoviesList = new List<MovieModel>();
            HttpResponseMessage response2 = _Client.GetAsync($"{_Client.BaseAddress}/Movie/Latest").Result;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;

                LatestMoviesList = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            ViewBag.Movies = movieList;
            ViewBag.TopTenMovies = topTenMoviesList;
            ViewBag.LatestMovies = LatestMoviesList;


            return View("Movies");
        }
        #endregion

        #region SelectedMovie
        [HttpGet]
        [Route("/Movies/SelectedMovie/{MovieID}")]
        public IActionResult SelectedMovie(int MovieID)
        {
            AddAuthorizationHeader();

            MovieModel model = new MovieModel();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Movie/{MovieID}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                model = JsonConvert.DeserializeObject<MovieModel>(data);
            }

            return View("SelectedMovie",model);
        }
        #endregion

        #region SearchMovie
        [HttpGet]
        [Route("/Movie/SearchMovie")]
        public IActionResult SearchMovie(string query)
        {
            AddAuthorizationHeader();

            List<MovieModel> movieList = new List<MovieModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Movie").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                movieList = JsonConvert.DeserializeObject<List<MovieModel>>(data);
            }

            var filteredMovies = movieList.Where(movie =>
                movie.MovieName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                movie.MovieCategory.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                movie.MovieLanguage.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                movie.MovieType.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.FilteredMovies = filteredMovies;

            return View("SearchMovie");
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
