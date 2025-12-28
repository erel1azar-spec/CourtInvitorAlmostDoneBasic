namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class ClientExistingHoursPage : ContentPage
{
	public ClientExistingHoursPage()
	{
		InitializeComponent();
		BindingContext = new ClientExistingHoursPageVM();
    }
}