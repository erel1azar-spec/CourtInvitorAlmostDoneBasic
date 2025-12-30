using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for displaying admin courts list.
    /// </summary>
    internal class AdminExistsCourtsVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly ObservableCollection<AdminExistsCourtsModel> courts;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of courts.
        /// </summary>
        public ObservableCollection<AdminExistsCourtsModel> Courts => courts;
        #endregion
        #region Commands
        public ICommand CourtSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the AdminExistsCourtsVM class.
        /// </summary>
        public AdminExistsCourtsVM()
        {
            courts = new ObservableCollection<AdminExistsCourtsModel>();
            CourtSelectedCommand = new Command<AdminExistsCourtsModel>(OnCourtSelected);
            NavBackHomeCommand = new Command(NavHome);
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Applies query attributes when navigating to this page.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Load();
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Loads the courts from Firestore.
        /// </summary>
        private async void Load()
        {
            string clubName = Preferences.Get(Keys.AdminSelectedClub, string.Empty);
            string date = Preferences.Get(Keys.AdminSelectedDate, string.Empty);
            List<AdminExistsCourtsModel> result = await AdminExistsCourts.LoadCourtsForDateAsync(clubName, date);
            courts.Clear();
            foreach (AdminExistsCourtsModel model in result)
                courts.Add(model);
        }
        /// <summary>
        /// Handles court selection.
        /// </summary>
        /// <param name="selectedCourt">The selected court.</param>
        private void OnCourtSelected(AdminExistsCourtsModel selectedCourt)
        {
            if (selectedCourt != null)
            {
                Preferences.Set(Keys.AdminSelectedCourt, selectedCourt.CourtNumber);
                Shell.Current.GoToAsync("///AdminExistsClients?refresh=true");
            }
        }
        /// <summary>
        /// Navigates back to the dates page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistsDates?refresh=true");
        }
        #endregion
    }
}
