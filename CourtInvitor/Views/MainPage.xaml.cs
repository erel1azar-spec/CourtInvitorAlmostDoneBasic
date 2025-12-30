using CourtInvitor.ViewModels;
namespace CourtInvitor.Views;
/// <summary>
/// Main page of the application.
/// </summary>
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainPageVM();
    }
}