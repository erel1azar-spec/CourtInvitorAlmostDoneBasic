namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class NavigataionPageAdmin : ContentPage
{
	public NavigataionPageAdmin()
	{
		InitializeComponent();
		BindingContext=new NavigataionPageAdminVM();
	}
}