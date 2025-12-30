using CourtInvitor.Models;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the client reservations page.
    /// </summary>
    internal class ClientReservationsPageVM : ObservableObject
    {
        #region Commands
        public ICommand NavBackHomeCommand => new Command(NavHome);
        #endregion
        #region Private Functions
        /// <summary>
        /// Navigates back to the client navigation page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigationPageClient?refresh=true");
        }
        #endregion
    }
}
