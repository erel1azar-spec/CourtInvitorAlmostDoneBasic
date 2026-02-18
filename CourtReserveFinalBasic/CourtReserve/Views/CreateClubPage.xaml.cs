namespace CourtReserve.Views;
using CourtReserve.ViewModels;

public partial class CreateClubPage : ContentPage
{
	public CreateClubPage()
	{
		InitializeComponent();
		BindingContext = new CreateClubPageVM();
    }
}