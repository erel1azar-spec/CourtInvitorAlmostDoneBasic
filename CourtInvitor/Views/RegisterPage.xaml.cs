using CourtInvitor.ViewModels;
namespace CourtInvitor.Views;
/// <summary>
/// Registration page for new users.
/// </summary>
public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterPageVM();
    }
}