using CourtReserve.ViewModels;

namespace CourtReserve.Views;

public partial class AdminExistingDatesPage : ContentPage
{
	public AdminExistingDatesPage()
	{
		InitializeComponent();
		BindingContext=new AdminExistingDatesPageVM();
	}
}