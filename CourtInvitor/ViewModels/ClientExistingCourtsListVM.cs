using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the client existing courts list page.
    /// </summary>
    internal class ClientExistingCourtsListVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly ObservableCollection<ClientExistingCourtsListModel> courts;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of courts.
        /// </summary>
        public ObservableCollection<ClientExistingCourtsListModel> Courts => courts;
        #endregion
        #region Commands
        public ICommand CourtSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ClientExistingCourtsListVM class.
        /// </summary>
        public ClientExistingCourtsListVM()
        {
            courts = new ObservableCollection<ClientExistingCourtsListModel>();
            CourtSelectedCommand = new Command<ClientExistingCourtsListModel>(OnCourtSelected);
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
        /// Loads courts from Firestore.
        /// </summary>
        private async void Load()
        {
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
            List<ClientExistingCourtsListModel> result = await ClientExistingCourtsList.LoadCourtsAsync(clubName);
            courts.Clear();
            foreach (ClientExistingCourtsListModel model in result)
                courts.Add(model);
        }
        /// <summary>
        /// Handles court selection.
        /// </summary>
        /// <param name="selectedCourt">The selected court.</param>
        private void OnCourtSelected(ClientExistingCourtsListModel selectedCourt)
        {
            Preferences.Set(Keys.ClientSelectedCourt, selectedCourt.CourtNumber);
            Shell.Current.GoToAsync("///ClientExistingHoursPage?refresh=true");
        }
        /// <summary>
        /// Navigates back to the dates list page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///ClientExistingDatesList?refresh=true");
        }
        #endregion
    }
}
