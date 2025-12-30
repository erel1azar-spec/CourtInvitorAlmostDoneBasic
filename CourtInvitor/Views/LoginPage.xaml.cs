using CourtInvitor.ViewModels;
namespace CourtInvitor.Views;
/// <summary>
/// Login page for user authentication.
/// </summary>
public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginPageVM();
    }
}