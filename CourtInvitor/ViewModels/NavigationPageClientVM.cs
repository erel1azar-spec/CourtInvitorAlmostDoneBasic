using System.Windows.Input;
using CourtInvitor.ModelsLogic;
using CourtInvitor.Models;

namespace CourtInvitor.ViewModels
{
    internal class NavigationPageClientVM: ObservableObject
    {
        public ICommand NavToMakeReservationCommand => new Command(NavToMakeReservation);
        public ICommand NavToPlayerSearchCommand => new Command(NavToPlayerSearch);
        public ICommand NavToMyReservationsCommand => new Command(NavToMyReservations);
        public ICommand NavToProfileCommand => new Command(NavToProfile);
        public ICommand NavToReceivedSuggastionsCommand => new Command(NavToReceivedSuggastions);
        public ICommand NavBackHomeCommand => new Command(NavHome);
        private readonly Session session;

        public string TimeLeft => session.TimeLeft;

        public NavigationPageClientVM()
        {
            session = new Session(); // singleton
            session.TimeLeftChanged += OnTimeLeftChanged;
            session.SessionExpired += OnSessionExpired;
        }

        private void OnTimeLeftChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TimeLeft));
        }

        private void OnSessionExpired(object? sender, EventArgs e)
        {
            Shell.Current.GoToAsync("///LoginPage");
        }


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

