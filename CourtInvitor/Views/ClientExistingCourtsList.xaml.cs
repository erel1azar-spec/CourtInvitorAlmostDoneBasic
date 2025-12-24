namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class ClientExistingCourtsList : ContentPage
{
	public ClientExistingCourtsList()
	{
		InitializeComponent();
		BindingContext = new ClientExistingCourtsListVM();
    }
}