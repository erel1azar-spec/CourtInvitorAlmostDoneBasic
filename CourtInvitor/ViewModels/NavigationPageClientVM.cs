using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the client navigation page.
    /// </summary>
    internal class NavigationPageClientVM : ObservableObject
    {
        #region Fields
        private readonly Session session;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the remaining session time.
        /// </summary>
        public string TimeLeft => session.TimeLeft;
        #endregion
        #region Commands
        public ICommand NavToMakeReservationCommand => new Command(NavToMakeReservation);
        public ICommand NavToPlayerSearchCommand => new Command(NavToPlayerSearch);
        public ICommand NavToMyReservationsCommand => new Command(NavToMyReservations);
        public ICommand NavToProfileCommand => new Command(NavToProfile);
        public ICommand NavToReceivedSuggastionsCommand => new Command(NavToReceivedSuggastions);
        public ICommand NavBackHomeCommand => new Command(NavHome);
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the NavigationPageClientVM class.
        /// </summary>
        public NavigationPageClientVM()
        {
            session = new Session();
            session.TimeLeftChanged += OnTimeLeftChanged;
            session.SessionExpired += OnSessionExpired;
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Handles the time left changed event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnTimeLeftChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TimeLeft));
        }
        /// <summary>
        /// Handles the session expired event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSessionExpired(object? sender, EventArgs e)
        {
            Shell.Current.GoToAsync("///LoginPage");
        }
        /// <summary>
        /// Navigates to the make reservation page.
        /// </summary>
        private async void NavToMakeReservation()
        {
            await Shell.Current.GoToAsync("///ClientExistingClubList?refresh=true");
        }
        /// <summary>
        /// Navigates to the player search page.
        /// </summary>
        private async void NavToPlayerSearch()
        {
            await Shell.Current.GoToAsync("///ClientPlayerSearchPage?refresh=true");
        }
        /// <summary>
        /// Navigates to the reservations page.
        /// </summary>
        private async void NavToMyReservations()
        {
            await Shell.Current.GoToAsync("///ClientReservationsPage?refresh=true");
        }
        /// <summary>
        /// Navigates to the profile page.
        /// </summary>
        private async void NavToProfile()
        {
            await Shell.Current.GoToAsync("///ProfileClientPage?refresh=true");
        }
        /// <summary>
        /// Navigates to the received suggestions page.
        /// </summary>
        private async void NavToReceivedSuggastions()
        {
            await Shell.Current.GoToAsync("///ReceivedSuggastionsClientPage?refresh=true");
        }
        /// <summary>
        /// Navigates back to the home page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }
        #endregion
    }
}
