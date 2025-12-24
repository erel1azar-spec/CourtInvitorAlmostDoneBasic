using CourtInvitor.ViewModels;

namespace CourtInvitor.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainPageVM();
    }
}