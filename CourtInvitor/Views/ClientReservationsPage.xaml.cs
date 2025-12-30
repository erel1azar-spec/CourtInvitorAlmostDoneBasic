namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;
/// <summary>
/// Page displaying client reservations.
/// </summary>
public partial class ClientReservationsPage : ContentPage
{
	public ClientReservationsPage()
	{
		InitializeComponent();
		BindingContext = new ClientReservationsPageVM();
    }
}