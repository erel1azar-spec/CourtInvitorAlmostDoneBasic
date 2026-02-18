using CourtReserve.ViewModels;
namespace CourtReserve.Views;

public partial class AdminExistingCourtsPage : ContentPage
{
	public AdminExistingCourtsPage()
	{
		InitializeComponent();
		BindingContext = new AdminExistingCourtsPageVM();
	}
}