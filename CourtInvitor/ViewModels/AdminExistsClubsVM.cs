using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the admin existing clubs page.
    /// </summary>
    internal class AdminExistsClubsVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly AdminExistsClubs adminExistsClubs;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the club name.
        /// </summary>
        public string ClubName => adminExistsClubs.Name;
        #endregion
        #region Commands
        public ICommand NavToDateCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the AdminExistsClubsVM class.
        /// </summary>
        public AdminExistsClubsVM()
        {
            adminExistsClubs = new AdminExistsClubs();
            NavToDateCommand = new Command(NavToDate);
            NavBackHomeCommand = new Command(NavHome);
            Load();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Applies query attributes when navigating to this page.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("refresh"))
                Load();
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Loads the club data.
        /// </summary>
        private async void Load()
        {
            string email = Preferences.Get(Keys.EmailKey, string.Empty);
            if (email != string.Empty)
            {
                await adminExistsClubs.LoadByUserEmailAsync(email);
                OnPropertyChanged(nameof(ClubName));
                Preferences.Set(Keys.AdminSelectedClub, ClubName);
            }
        }
        /// <summary>
        /// Navigates to the dates page.
        /// </summary>
        private async void NavToDate()
        {
            await Shell.Current.GoToAsync("///AdminExistsDates?refresh=true");
        }
        /// <summary>
        /// Navigates back to the admin navigation page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigationPageAdmin?refresh=true");
        }
        #endregion
    }
}
