using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class NavigationPageClientVM
    {
        public ICommand NavToMakeReservationCommand => new Command(NavToMakeReservation);
        public ICommand NavToPlayerSearchCommand => new Command(NavToPlayerSearch);
        public ICommand NavToMyReservationsCommand => new Command(NavToMyReservations);
        public ICommand NavToProfileCommand => new Command(NavToProfile);
        public ICommand NavToReceivedSuggastionsCommand => new Command(NavToReceivedSuggastions);
        public ICommand NavBackHomeCommand => new Command(NavHome);


        private async void NavToMakeReservation()
        {
            await Shell.Current.GoToAsync("///ClientExistingClubList?refresh=true");
        }
        private async void NavToPlayerSearch()
        {
            await Shell.Current.GoToAsync("///ClientPlayerSearchPage?refresh=true");
        }
        private async void NavToMyReservations()
        {
            await Shell.Current.GoToAsync("///ClientReservationsPage?refresh=true");
        }
        private async void NavToProfile()
        {
            await Shell.Current.GoToAsync("///ProfileClientPage?refresh=true");
        }
        private async void NavToReceivedSuggastions()
        {
            await Shell.Current.GoToAsync("///ReceivedSuggastionsClientPage?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }

    }
}

