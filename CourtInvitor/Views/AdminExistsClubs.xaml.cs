namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;
/// <summary>
/// Page displaying existing clubs for admin.
/// </summary>
public partial class AdminExistsClubs : ContentPage
{
	public AdminExistsClubs()
	{
		InitializeComponent();
		BindingContext = new AdminExistsClubsVM();
    }
}