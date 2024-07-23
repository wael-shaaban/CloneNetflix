using CloneNetflix.Helpers;
using CloneNetflix.Pages;
using CloneNetflix.Services;
using CloneNetflix.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CloneNetflix
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient(AppConstants.TmdvHttpClientName,
                httpClient => httpClient.BaseAddress = new Uri("https://api.themoviedb.org"));

            builder.Services.TryAddSingleton<TmdbService>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddSingleton<CategoriesPage>();
            builder.Services.AddSingleton<CategoriesViewModel>();
            builder.Services.AddTransientWithShellRoute<DetailsPage, DetailsViewModel>(nameof(DetailsPage));
            return builder.Build();
        }
    }
}