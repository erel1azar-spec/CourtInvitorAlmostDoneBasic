namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class MakeReservationClientPage : ContentPage
{
	public MakeReservationClientPage()
	{
		InitializeComponent();
		BindingContext = new MakeReservationClientPageVM();
    }
}