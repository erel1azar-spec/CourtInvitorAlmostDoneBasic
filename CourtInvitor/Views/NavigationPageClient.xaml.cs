using CourtInvitor.ViewModels;
namespace CourtInvitor.Views;
/// <summary>
/// Navigation page for client users.
/// </summary>
public partial class NavigationPageClient : ContentPage
{
	public NavigationPageClient()
	{
        InitializeComponent();
        BindingContext = new NavigationPageClientVM();
    }
}