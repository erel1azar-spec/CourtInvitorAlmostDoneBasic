namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;
/// <summary>
/// Page displaying list of existing clubs for clients.
/// </summary>
public partial class ClientExistingClubList : ContentPage
{
	public ClientExistingClubList()
	{
		InitializeComponent();
		BindingContext = new ClientExistingClubListVM();
    }
}