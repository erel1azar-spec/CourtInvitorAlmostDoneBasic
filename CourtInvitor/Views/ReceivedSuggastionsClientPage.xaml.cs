namespace CourtInvitor.Views;
using CourtInvitor.ViewModels;

public partial class ReceivedSuggastionsClientPage : ContentPage
{
	public ReceivedSuggastionsClientPage()
	{
		InitializeComponent();
		BindingContext = new ReceivedSuggastionsClientPageVM();
    }
}