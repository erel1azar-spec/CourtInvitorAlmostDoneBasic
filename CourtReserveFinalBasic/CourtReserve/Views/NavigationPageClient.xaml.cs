namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class NavigationPageClient : ContentPage
{
	public NavigationPageClient()
	{
		InitializeComponent();
		BindingContext = new NavigationPageClientVM();
    }
}