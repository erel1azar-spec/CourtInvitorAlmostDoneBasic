using CourtInvitor.ViewModels;

namespace CourtInvitor.Views;

public partial class NavigationPageClient : ContentPage
{
	public NavigationPageClient()
	{
        InitializeComponent();
        BindingContext = new NavigationPageClientVM();
    }
}