namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class ClientExistingCourtsPage : ContentPage
{
	public ClientExistingCourtsPage()
	{
		InitializeComponent();
		BindingContext = new ClientExistingCourtsPageVM();
    }
}