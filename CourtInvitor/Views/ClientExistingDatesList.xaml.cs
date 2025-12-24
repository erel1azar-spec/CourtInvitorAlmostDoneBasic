namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class ClientExistingDatesList : ContentPage
{
	public ClientExistingDatesList()
	{
		InitializeComponent();
		BindingContext = new ClientExistingDatesListVM();
    }
}