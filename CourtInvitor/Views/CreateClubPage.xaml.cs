namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class CreateClubPage : ContentPage
{
	public CreateClubPage()
	{
		InitializeComponent();
		BindingContext = new CreateClubPageVM();
    }
}