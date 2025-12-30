using CourtInvitor.Models;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the admin navigation page.
    /// </summary>
    internal class NavigationPageAdminVM : ObservableObject
    {
        #region Commands
        public ICommand NavToReservationsMadeCommand => new Command(NavToReservationsMade);
        public ICommand NavToCreateCourtCommand => new Command(NavToCreateCourt);
        public ICommand NavBackHomeCommand => new Command(NavHome);
        #endregion
        #region Private Functions
        /// <summary>
        /// Navigates to the reservations made page.
        /// </summary>
        private async void NavToReservationsMade()
        {
            await Shell.Current.GoToAsync("///AdminExistsClubs?refresh=true");
        }
        /// <summary>
        /// Navigates to the create court page.
        /// </summary>
        private async void NavToCreateCourt()
        {
            await Shell.Current.GoToAsync("///CreateClubPage?refresh=true");
        }
        /// <summary>
        /// Navigates back to the main page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }
        #endregion
    }
}
