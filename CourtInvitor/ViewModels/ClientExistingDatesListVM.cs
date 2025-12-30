using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the client existing dates list page.
    /// </summary>
    internal class ClientExistingDatesListVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly ObservableCollection<ClientExistingDatesListModel> dates;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of dates.
        /// </summary>
        public ObservableCollection<ClientExistingDatesListModel> DatesClient => dates;
        #endregion
        #region Commands
        public ICommand DateSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ClientExistingDatesListVM class.
        /// </summary>
        public ClientExistingDatesListVM()
        {
            dates = new ObservableCollection<ClientExistingDatesListModel>();
            NavBackHomeCommand = new Command(NavHome);
            DateSelectedCommand = new Command<string>(OnDateSelected);
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
        /// Loads dates from Firestore.
        /// </summary>
        private async void Load()
        {
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
            List<ClientExistingDatesListModel> result = await ClientExistingDatesList.LoadClientDatesAsync(clubName);
            dates.Clear();
            foreach (ClientExistingDatesListModel model in result)
                dates.Add(model);
        }
        /// <summary>
        /// Handles date selection.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void OnDateSelected(string selectedDate)
        {
            Preferences.Set(Keys.ClientSelectedDate, selectedDate);
            Shell.Current.GoToAsync("///ClientExistingCourtsList?refresh=true");
        }
        /// <summary>
        /// Navigates back to the club list page.
        /// </summary>
        private async void NavHome()
        {
            Preferences.Clear(Keys.ClientSelectedClub);
            await Shell.Current.GoToAsync("///ClientExistingClubList?refresh=true");
        }
        #endregion
    }
}
