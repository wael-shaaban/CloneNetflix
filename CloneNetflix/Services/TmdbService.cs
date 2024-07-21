using CloneNetflix.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CloneNetflix.Services.ServiceModels;
using System.Net.Http.Json;
using CloneNetflix.Models;
using System.Dynamic;
namespace CloneNetflix.Services
{
    public class TmdbService
    {
        private const string ApiKey = "9e0af884aaa6c4cff61dd544e22246d6";//get it from tmdb website.
        private readonly IHttpClientFactory _httpClientFactory;
        public TmdbService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient HttpClient => _httpClientFactory.CreateClient(AppConstants.TmdvHttpClientName);

        public async Task<IEnumerable<Genre>> GetCategoriesAsync()
        {
            var data =await HttpClient.GetFromJsonAsync<GenreWrapper>($"{TmdbUrls.MovieGenres}&api_key={ApiKey}");
            return data.Genres;
        }
        public async Task<IEnumerable<Media>> GetTrendingAsync() =>
            await GetMediasAsync(TmdbUrls.Trending);

        public async Task<IEnumerable<Media>> GetTopRatedAsync()=>
             await GetMediasAsync(TmdbUrls.TopRated);

        public async Task<IEnumerable<Media>> GetActionMoviesAsync()=>
               await GetMediasAsync(TmdbUrls.Action);

        public async Task<IEnumerable<Media>> GetNetflixOriginalsAsync()=>
               await GetMediasAsync(TmdbUrls.NetflixOriginals);

        private async Task<IEnumerable<Media>> GetMediasAsync(string url)
        {
            var trendingMoviesCollections = await HttpClient.GetFromJsonAsync<Movie>($"{url}&api_key={ApiKey}");
            return trendingMoviesCollections.results.Select(x => x.ResultToMediaObject());
        }
    }
}