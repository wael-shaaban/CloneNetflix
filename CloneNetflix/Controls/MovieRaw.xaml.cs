using CloneNetflix.Models;
using CloneNetflix.Services.ServiceModels;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CloneNetflix.Controls;
public class MediaSelectedEventArgs : EventArgs
{
    public Media Media { get; init; }
    public MediaSelectedEventArgs(Media media) => Media = media;
}
public partial class MovieRaw : ContentView
{
    public static readonly BindableProperty MoviesProperty = BindableProperty.Create(nameof(Movies), typeof(IEnumerable<Media>), typeof(MovieRaw), default(IEnumerable<Media>));
    public static readonly BindableProperty HeadingProperty = BindableProperty.Create(nameof(Heading), typeof(string), typeof(MovieRaw), string.Empty);
    public static readonly BindableProperty IsLargeProperty = BindableProperty.Create(nameof(IsLarge), typeof(bool), typeof(MovieRaw), false);
    public string Heading
    {
        get => (string)GetValue(MovieRaw.HeadingProperty);
        set => SetValue(MovieRaw.HeadingProperty, value);
    }
    public IEnumerable<Media> Movies
    {
        get => (IEnumerable<Media>)GetValue(MovieRaw.MoviesProperty);
        set => SetValue(MovieRaw.MoviesProperty, value);
    }
    public bool IsLarge
    {
        get => (bool)GetValue(MovieRaw.IsLargeProperty);
        set => SetValue(MovieRaw.IsLargeProperty, value);
    }
    public bool IsNotLarge => !IsLarge;

    public event EventHandler<MediaSelectedEventArgs> MediaSelected;

    public MovieRaw()
    {
        InitializeComponent();
        MediaDetailsCommand = new Command(ExecuteMediaDetailsCommand);
    }

    public ICommand MediaDetailsCommand { get; private set; }
    private void ExecuteMediaDetailsCommand(object parameter)
    {
        if (parameter is Media media && media is not null)
        {
            MediaSelected?.Invoke(this, new MediaSelectedEventArgs(media));
        }
    }
}