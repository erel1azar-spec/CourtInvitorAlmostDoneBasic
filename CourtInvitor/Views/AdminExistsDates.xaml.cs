namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class AdminExistsDates : ContentPage
{
	public AdminExistsDates()
	{
		InitializeComponent();
		BindingContext=new AdminExistsDatesVM();
    }
}