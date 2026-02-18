using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CourtReserve.Models;
using CourtReserve.ModelsLogic;

namespace CourtReserve.ViewModels
{
    public class NavigationPageClientVM: ObservableObject
    {
        private readonly ClubsModel clubsModel = new Clubs();

        public ICommand NavToMakeReservationCommand => new Command(NavToMakeReservation);
        public ICommand NavToPlayerSearchCommand => new Command(NavToPlayerSearch);
        public ICommand NavToMyReservationsCommand => new Command(NavToMyReservations);
        public ICommand NavToProfileCommand => new Command(NavToProfile);
        public ICommand NavToReceivedSuggastionsCommand => new Command(NavToReceivedSuggastions);
        public ICommand NavBackHomeCommand => new Command(NavHome);
        private void OnSessionExpired(object? sender, EventArgs e)
        {
            Shell.Current.GoToAsync("///LoginPage");
        }
        private async void NavToMakeReservation()
        {
            await clubsModel.EnsureAllClubsHaveDocumentsAsync();
            await Shell.Current.GoToAsync("///ClientExistingClubsPage?refresh=true");
        }
        private async void NavToPlayerSearch()
        {
            await Shell.Current.GoToAsync("///ClientPlayerSearchPage?refresh=true");
        }
        private async void NavToMyReservations()
        {
            await Shell.Current.GoToAsync("///ClientExistingReservationsPage?refresh=true");
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
            await Shell.Current.GoToAsync("///LoginPage?refresh=true");
        }
    }
}
