using CourtReserve.Models;
using CourtReserve.ModelsLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtReserve.ViewModels
{
    internal class NavigationPageAdminVM:ObservableObject
    {
        User user=new User();
       
        public ICommand NavToReservationsMadeCommand => new Command(NavToReservationsMade);
        public ICommand NavToCreateCourtCommand => new Command(NavToCreateCourt);
        public ICommand SignOutCommend => new Command(SignOut);
        /// <summary>
        /// Navigates to the reservations made page.
        /// </summary>
        private async void NavToReservationsMade()
        {
            await Shell.Current.GoToAsync("///AdminExistingClubsPage?refresh=true");
        }
        private async void NavToCreateCourt()
        {
            await Shell.Current.GoToAsync("///CreateClubPage?refresh=true");
        }
        private async void SignOut()
        {
            user.SignOut();
            await Shell.Current.GoToAsync("///LoginPage?refresh=true");
        }
    }
}
