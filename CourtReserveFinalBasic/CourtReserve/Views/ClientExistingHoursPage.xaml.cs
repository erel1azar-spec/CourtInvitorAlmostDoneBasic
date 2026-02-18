using CourtReserve.ViewModels;

namespace CourtReserve.Views;

public partial class ClientExistingHoursPage : ContentPage
{
	public ClientExistingHoursPage()
	{
		InitializeComponent();
		BindingContext = new ClientExistingHoursPageVM();

    }
}