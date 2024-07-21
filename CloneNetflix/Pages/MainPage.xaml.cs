using CloneNetflix.Services;
using CloneNetflix.ViewModels;

namespace CloneNetflix.Pages
{
    public partial class MainPage : ContentPage
    {
        HomeViewModel HomeViewModel;
        public MainPage(HomeViewModel homeViewModel)
        {
            this.BindingContext = homeViewModel;
            HomeViewModel = homeViewModel;
            InitializeComponent();
        }

        private void MovieRaw_MediaSelected(object sender, Controls.MediaSelectedEventArgs e)
        {
            HomeViewModel.SelectMedia(e.Media);
        }

        private void PopUpMovie_Closed(object sender, EventArgs e)
        {
            HomeViewModel.SelectMedia(null);
        }
    }
}
