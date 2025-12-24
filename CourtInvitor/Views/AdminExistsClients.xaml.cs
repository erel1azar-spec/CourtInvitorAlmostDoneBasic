namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class AdminExistsClients : ContentPage
{
	public AdminExistsClients()
	{
		InitializeComponent();
		BindingContext = new AdminExistsClientsVM();
    }
}