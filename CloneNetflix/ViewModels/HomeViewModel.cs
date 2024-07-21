using CloneNetflix.Models;
using CloneNetflix.Pages;
using CloneNetflix.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneNetflix.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly TmdbService tmdbService;
        public HomeViewModel(TmdbService _tmdbService)
        {
            tmdbService = _tmdbService;
        }
        [ObservableProperty]
        Media trendingMovie;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(ShowMovieInfoBox))]
        Media? selectedMedia;
        public bool ShowMovieInfoBox => SelectedMedia is not null;
        public ObservableCollection<Media> Trendings { get; set; } = new();
        public ObservableCollection<Media> TopRated { get; set; } = new();
        public ObservableCollection<Media> ActionMovies { get; set; } = new();
        public ObservableCollection<Media> NetflixOriginals { get; set; } = new();
        [RelayCommand]
        public async Task InitializeAsync()
        {
            Trendings = new ObservableCollection<Media>();
            var trendingsResult = tmdbService.GetTrendingAsync();
            var topratedResult = tmdbService.GetTopRatedAsync();
            var actionResult = tmdbService.GetActionMoviesAsync();
            var originalsResult = tmdbService.GetNetflixOriginalsAsync();
            var alldata = await Task.WhenAll(trendingsResult, topratedResult, actionResult, originalsResult);
            var trendinglist = alldata[0];
            var topratedresult = alldata[1];
            var actionlist = alldata[2];
            var originallist = alldata[3];
            TrendingMovie = trendinglist.OrderBy(x => Guid.NewGuid()).FirstOrDefault(x => !string.IsNullOrEmpty(x.DisplayTitle) && !string.IsNullOrEmpty(x.Thumbnail));
            // SelectedMedia =TrendingMovie;
            FillCollection(trendinglist, Trendings);
            FillCollection(topratedresult, TopRated);
            FillCollection(actionlist, ActionMovies);
            FillCollection(originallist, NetflixOriginals);
        }
        private static void FillCollection(IEnumerable<Media> medias, ObservableCollection<Media> collection)
        {
            if (medias is not null && collection is not null)
            {
                if (medias.Any())
                {
                    collection.Clear();
                    foreach (var t in medias)
                    {
                        collection.Add(t);
                    }
                }
            }
        }
        [RelayCommand]
        public async void SelectMedia(Media? media = null)
        {
            if (media is not null)
                if (media.Id == SelectedMedia?.Id)
                    media = null;
            SelectedMedia = media;
        }
        [RelayCommand]
        public async Task Category()=>await Shell.Current.GoToAsync(nameof(CategoriesPage));
    }
}