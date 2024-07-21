using CloneNetflix.ViewModels;

namespace CloneNetflix.Pages;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesViewModel categoriesViewModel)
	{
		InitializeComponent();
		this.BindingContext = categoriesViewModel;
	}
}