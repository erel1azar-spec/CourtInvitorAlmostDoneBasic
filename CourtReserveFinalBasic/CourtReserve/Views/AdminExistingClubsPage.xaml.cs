namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class AdminExistingClubsPage : ContentPage
{
	public AdminExistingClubsPage()
	{
		InitializeComponent();
		BindingContext = new AdminExistingClubsPageVM();
    }
}