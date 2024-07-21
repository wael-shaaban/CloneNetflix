using CloneNetflix.Services;
using CloneNetflix.Services.ServiceModels;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloneNetflix.ViewModels
{
    public partial class CategoriesViewModel : BaseViewModel
    {
        private IEnumerable<Genre> genreList;   
        [RelayCommand]
        public async Task Close() => await Shell.Current.GoToAsync("../");
        public ObservableCollection<string> Categories { get; set; } = new();
        private readonly TmdbService tmdbService;
        public CategoriesViewModel(TmdbService _tmdbService)
        {
             tmdbService = _tmdbService ?? throw new ArgumentNullException(nameof(tmdbService)); 
        }
        [RelayCommand]
        public async Task Initialize()
        {
            if (genreList is null)
                genreList = await tmdbService.GetCategoriesAsync();
          //  Preferences.Default.Set("Categories", genreList);
            Categories.Clear();
            Categories.Add("MyList");
            Categories.Add("Downloads");
            foreach (var category in genreList)
                Categories.Add(category.Name);  
        }
    }
}