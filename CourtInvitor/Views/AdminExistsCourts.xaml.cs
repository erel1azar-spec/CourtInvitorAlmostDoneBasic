namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class AdminExistsCourts : ContentPage
{
	public AdminExistsCourts()
	{
		InitializeComponent();
		BindingContext=new AdminExistsCourtsVM();
    }
}