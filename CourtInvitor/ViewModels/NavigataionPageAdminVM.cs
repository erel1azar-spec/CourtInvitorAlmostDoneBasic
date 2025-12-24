using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class NavigataionPageAdminVM
    {
        public ICommand NavToReservationsMadeCommand => new Command(NavToReservationsMade);
        public ICommand NavToCreateCourtCommand => new Command(NavToCreateCourt);
        public ICommand NavToProfileCommand => new Command(NavToProfile);
        public ICommand NavBackHomeCommand => new Command(NavHome);



        private async void NavToReservationsMade()
        {
            await Shell.Current.GoToAsync("///AdminExistsClubs?refresh=true");
        }
        private async void NavToCreateCourt()
        {
            await Shell.Current.GoToAsync("///CreateClubPage?refresh=true");
        }
        private async void NavToProfile()
        {
            await Shell.Current.GoToAsync("///ProfileClientPage?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }
    }
}
