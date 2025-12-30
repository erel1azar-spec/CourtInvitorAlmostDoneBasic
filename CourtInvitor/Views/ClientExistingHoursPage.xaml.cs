namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;
/// <summary>
/// Page displaying available hours for booking.
/// </summary>
public partial class ClientExistingHoursPage : ContentPage
{
	public ClientExistingHoursPage()
	{
		InitializeComponent();
		BindingContext = new ClientExistingHoursPageVM();
    }
}