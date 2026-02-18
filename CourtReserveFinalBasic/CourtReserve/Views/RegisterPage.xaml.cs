using CourtReserve.ViewModels;

namespace CourtReserve.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterPageVM();

    }
}