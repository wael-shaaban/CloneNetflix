using CloneNetflix.Models;
using CloneNetflix.Pages;
using CloneNetflix.Services;
using CloneNetflix.Services.ServiceModels;
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
    [QueryProperty(nameof(Media), nameof(Media))]
    public partial class DetailsViewModel : BaseViewModel
    {
        private readonly TmdbService tmdbService;

        [ObservableProperty]
        private Media media;

        [ObservableProperty]
        string? trailerUrl;

        [ObservableProperty]
        int? runTime;

        [ObservableProperty]
        int widthSimilarItem = 135;

        public ObservableCollection<Video> Videos { get; set; } = new();
        public DetailsViewModel(TmdbService tmdbService)
        {
            this.tmdbService = tmdbService;
        }
        public ObservableCollection<Media> Similars { get; set; } = new();
        [RelayCommand]
        public async Task Initialize()
        {
            var similarTask = tmdbService.GetSimilarAsync(media.Id, media.MediaType);
            IsBusy = true;
            try
            {
                var trailerTaks = tmdbService.GetTrailersAsyns(Media.Id, Media.MediaType);
                var movieDetailsTask = tmdbService.GetMovieDetailsAsync(Media.Id, Media.MediaType);
                var trailers = await trailerTaks;
                var movieDetails = await movieDetailsTask;
                if (trailers?.Any() == true)
                {
                    var _mytrailer = trailers.Where(x => x.type == "Trailer").FirstOrDefault();
                    _mytrailer ??= trailers.FirstOrDefault();
                    TrailerUrl = GetYoutubeUrl(_mytrailer?.key);
                    foreach (var trailer in trailers)
                        Videos.Add(trailer);
                }
                else await Shell.Current.DisplayAlert("Not Found", "Trailer Not Fount", "Ok");
                RunTime = movieDetails?.runtime;
            }
            finally
            {
                IsBusy = false;
            }
            var data = await similarTask;
            if (data?.Any() == true)
                foreach (var item in data)
                    Similars.Add(item);
        }
        private string GetYoutubeUrl(string Videokey) =>
            $"https://www.youtube.com/embed/{Videokey}";
        [RelayCommand]
        public async Task ChangeToThisMedia(Media _media)
        {
            if (_media == null) return;

            var media = new ShellNavigationQueryParameters
            {
                [nameof(Media)] = _media
            };
            await Shell.Current.GoToAsync(nameof(DetailsPage), true, media);
        }
        [RelayCommand]  
        public void ChangeToThiTrailer(string keyVideo) =>
              TrailerUrl = GetYoutubeUrl(keyVideo);
    }
}