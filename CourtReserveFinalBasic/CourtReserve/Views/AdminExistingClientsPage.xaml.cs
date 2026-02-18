namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class AdminExistingClientsPage : ContentPage
{
	public AdminExistingClientsPage()
	{
		InitializeComponent();
		BindingContext = new AdminExistingClientsPageVM();
    }
}