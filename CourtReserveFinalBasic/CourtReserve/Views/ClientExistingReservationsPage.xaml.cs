namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class ClientExistingReservationsPage : ContentPage
{
	public ClientExistingReservationsPage()
	{
		InitializeComponent();
		BindingContext = new ClientExistingReservationsPageVM();

    }
}