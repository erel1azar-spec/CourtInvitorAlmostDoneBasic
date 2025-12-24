namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class AdminExistsClubs : ContentPage
{
	public AdminExistsClubs()
	{
		InitializeComponent();
		BindingContext = new AdminExistsClubsVM();
    }
}