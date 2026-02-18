namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginPageVM();
	}
}