namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;
/// <summary>
/// Admin navigation page for accessing admin features.
/// </summary>
public partial class NavigationPageAdmin : ContentPage
{
	public NavigationPageAdmin()
	{
		InitializeComponent();
		BindingContext = new NavigationPageAdminVM();
	}
}