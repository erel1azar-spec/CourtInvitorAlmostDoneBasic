namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;
/// <summary>
/// Page for creating a new club.
/// </summary>
public partial class CreateClubPage : ContentPage
{
	public CreateClubPage()
	{
		InitializeComponent();
		BindingContext = new CreateClubPageVM();
    }
}