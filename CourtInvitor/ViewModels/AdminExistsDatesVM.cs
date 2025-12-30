using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for displaying admin dates list.
    /// </summary>
    internal class AdminExistsDatesVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly ObservableCollection<AdminExistsDatesModel> dates;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of dates.
        /// </summary>
        public ObservableCollection<AdminExistsDatesModel> Dates => dates;
        #endregion
        #region Commands
        public ICommand DateSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the AdminExistsDatesVM class.
        /// </summary>
        public AdminExistsDatesVM()
        {
            dates = new ObservableCollection<AdminExistsDatesModel>();
            NavBackHomeCommand = new Command(NavHome);
            DateSelectedCommand = new Command<string>(OnDateSelected);
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
        /// Loads the dates from Firestore.
        /// </summary>
        private async void Load()
        {
            string clubName = Preferences.Get(Keys.AdminSelectedClub, string.Empty);
            List<AdminExistsDatesModel> result = await AdminExistsDates.LoadDatesAsync(clubName);
            dates.Clear();
            foreach (AdminExistsDatesModel model in result)
                dates.Add(model);
        }
        /// <summary>
        /// Handles date selection.
        /// </summary>
        /// <param name="selectedDate">The selected date.</param>
        private void OnDateSelected(string selectedDate)
        {
            Preferences.Set(Keys.AdminSelectedDate, selectedDate);
            Shell.Current.GoToAsync("///AdminExistsCourts?refresh=true");
        }
        /// <summary>
        /// Navigates back to the clubs page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistsClubs?refresh=true");
        }
        #endregion
    }
}
