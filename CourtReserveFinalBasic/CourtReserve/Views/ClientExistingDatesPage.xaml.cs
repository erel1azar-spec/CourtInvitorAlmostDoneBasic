namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class ClientExistingDatesPage : ContentPage
{
	public ClientExistingDatesPage()
	{
		InitializeComponent();
		BindingContext = new ClientExistingDatesPageVM();
    }
}