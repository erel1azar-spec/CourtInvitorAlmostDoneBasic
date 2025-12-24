namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class ClientReservationsPage : ContentPage
{
	public ClientReservationsPage()
	{
		InitializeComponent();
		BindingContext = new ClientReservationsPageVM();
    }
}