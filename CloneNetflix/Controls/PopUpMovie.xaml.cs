using CloneNetflix.Models;
using System.Windows.Input;

namespace CloneNetflix.Controls;

public partial class PopUpMovie : ContentView
{
	public static readonly BindableProperty MediaProperty = BindableProperty.Create(nameof(Media), typeof(Media), typeof(PopUpMovie), null);
	public Media Media
	{
		get => (Media)GetValue(MediaProperty);
		set => SetValue(MediaProperty, value);
	}
	public ICommand CloseCommand { get;set; }

	public event EventHandler Closed;	
    public PopUpMovie()
	{
		InitializeComponent();
		CloseCommand = new Command(ExecuteMediaDetailsCommand);
	}
    public ICommand MediaDetailsCommand { get; private set; }
	private void ExecuteMediaDetailsCommand() => Closed?.Invoke(this, EventArgs.Empty);

    private void Button_Clicked(object sender, EventArgs e) => Closed?.Invoke(this, EventArgs.Empty);
}