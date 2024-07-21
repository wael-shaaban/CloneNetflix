using CloneNetflix.Pages;

namespace CloneNetflix
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
        }
    }
}