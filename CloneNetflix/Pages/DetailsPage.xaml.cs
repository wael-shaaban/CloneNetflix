using CloneNetflix.ViewModels;

namespace CloneNetflix.Pages;

public partial class DetailsPage : ContentPage
{
    private readonly DetailsViewModel viewModel;
	public DetailsPage(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();
		this.BindingContext = detailsViewModel;
        viewModel = detailsViewModel;   
        similarTabIndicator.IsVisible = false;
	}
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (width > 0)
            viewModel.WidthSimilarItem = Convert.ToInt32(width / 3) - 10;

    }
    private void Trailer_Tapped(object sender, TappedEventArgs e)
    {
        similarTabIndicator.IsVisible = false;
        trailersTabIndicator.IsVisible = true;
        showSimilar.IsVisible = false;    
        showTrailer.IsVisible = true;
    }

    private void Similar_Tapped(object sender, TappedEventArgs e)
    {
        trailersTabIndicator.IsVisible = false;
        similarTabIndicator.IsVisible = true;
        showTrailer.IsVisible = false;
        showSimilar.IsVisible = true;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        this.pagescroll.ScrollToAsync(0, 0, animated: true);
    }
}