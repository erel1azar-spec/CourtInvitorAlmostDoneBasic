namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class ClientExistingClubsPage : ContentPage
{
	public ClientExistingClubsPage()
	{
		InitializeComponent();
		BindingContext = new ClientExistingClubsPageVM();
    }
}