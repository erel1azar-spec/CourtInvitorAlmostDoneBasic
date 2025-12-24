
namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;


public partial class ClientPlayerSearchPage : ContentPage
{
	public ClientPlayerSearchPage()
	{
		InitializeComponent();
        BindingContext = new ClientPlayerSearchPageVM();
    }
}