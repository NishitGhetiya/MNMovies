using Microsoft.AspNetCore.Mvc;
using MNMovies.Models;
using Newtonsoft.Json;

namespace MNMovies.Controllers.HomeControllers
{
    [CheckAccess]
    public class HomeSeriesController : Controller
    {

        #region Uri
        private readonly Uri baseAddress = new Uri("https://localhost:44301/api");
        private readonly HttpClient _Client;

        public HomeSeriesController()
        {
            _Client = new HttpClient { BaseAddress = baseAddress };
        }
        #endregion

        #region Series
        [Route("/Series")]
        public IActionResult Series()
        {
            AddAuthorizationHeader();

            List<SeriesModel> seriesList = new List<SeriesModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Series").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                seriesList = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }

            List<SeriesModel> topTenSeriesList = new List<SeriesModel>();
            HttpResponseMessage response1 = _Client.GetAsync($"{_Client.BaseAddress}/Series/TopTen").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;

                topTenSeriesList = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }

            List<SeriesModel> LatestSeriesList = new List<SeriesModel>();
            HttpResponseMessage response2 = _Client.GetAsync($"{_Client.BaseAddress}/Series/Latest").Result;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;

                LatestSeriesList = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }

            ViewBag.Series = seriesList;
            ViewBag.TopTenSeries = topTenSeriesList;
            ViewBag.LatestSeries = LatestSeriesList;

            return View("Series");
        }
        #endregion

        #region SelectedSeries
        [HttpGet]
        [Route("/Series/SelectedSeries/{SeriesID}")]
        public IActionResult SelectedSeries(int SeriesID)
        {
            AddAuthorizationHeader();

            SeriesModel model = new SeriesModel();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Series/{SeriesID}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                model = JsonConvert.DeserializeObject<SeriesModel>(data);
            }

            List<SeasonModel> seasonList = new List<SeasonModel>();
            HttpResponseMessage response1 = _Client.GetAsync($"{_Client.BaseAddress}/Season/Series/{SeriesID}").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;

                seasonList = JsonConvert.DeserializeObject<List<SeasonModel>>(data);
            }
            ViewBag.Seasons = seasonList;

            return View("SelectedSeries", model);
        }
        #endregion

        #region SelectedSeason
        [HttpGet]
        [Route("/Season/SelectedSeason/{SeasonID}")]
        public IActionResult SelectedSeason(int SeasonID)
        {
            AddAuthorizationHeader();

            SeasonModel model = new SeasonModel();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Season/{SeasonID}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                model = JsonConvert.DeserializeObject<SeasonModel>(data);
            }

            List<EpisodeModel> episodeList = new List<EpisodeModel>();
            HttpResponseMessage response1 = _Client.GetAsync($"{_Client.BaseAddress}/Episode/Season/{SeasonID}").Result;
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;

                episodeList = JsonConvert.DeserializeObject<List<EpisodeModel>>(data);
            }
            ViewBag.Episodes = episodeList;

            return View("SelectedSeason", model);
        }
        #endregion

        #region SearchSeries
        [HttpGet]
        [Route("/Series/SearchSeries")]
        public IActionResult SearchSeries(string query)
        {
            AddAuthorizationHeader();

            List<SeriesModel> seriesList = new List<SeriesModel>();
            HttpResponseMessage response = _Client.GetAsync($"{_Client.BaseAddress}/Series").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                seriesList = JsonConvert.DeserializeObject<List<SeriesModel>>(data);
            }

            var filteredSeries = seriesList.Where(series =>
                series.SeriesName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                series.SeriesCategory.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                series.SeriesLanguage.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                series.SeriesType.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewBag.FilteredSeries = filteredSeries;

            return View("SearchSeries");
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
