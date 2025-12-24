namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class ClientExistingClubList : ContentPage
{
	public ClientExistingClubList()
	{
		InitializeComponent();
		BindingContext = new ClientExistingClubListVM();
    }
}