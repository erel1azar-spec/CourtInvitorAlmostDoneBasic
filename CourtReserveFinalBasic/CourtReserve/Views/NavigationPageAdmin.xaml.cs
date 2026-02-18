using CourtReserve.ViewModels;
namespace CourtReserve.Views;

public partial class NavigationPageAdmin : ContentPage
{
	public NavigationPageAdmin()
	{
		InitializeComponent();
		BindingContext = new NavigationPageAdminVM();
	}
}