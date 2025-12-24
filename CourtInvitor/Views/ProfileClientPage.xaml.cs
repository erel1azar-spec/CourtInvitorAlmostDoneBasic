namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class ProfileClientPage : ContentPage
{
	public ProfileClientPage()
	{
		InitializeComponent();
		BindingContext = new ProfileClientPageVM();
    }
}