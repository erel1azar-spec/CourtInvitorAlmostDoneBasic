using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for displaying court hour bookings for admin.
    /// </summary>
    internal class AdminExistsClientsVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly FbData fbData;
        private readonly ObservableCollection<AdminHourSlotModel> hours;
        private string pageTitle;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of hour slots.
        /// </summary>
        public ObservableCollection<AdminHourSlotModel> Hours => hours;
        /// <summary>
        /// Gets the page title.
        /// </summary>
        public string PageTitle => pageTitle;
        #endregion
        #region Commands
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the AdminExistsClientsVM class.
        /// </summary>
        public AdminExistsClientsVM()
        {
            fbData = new FbData();
            hours = new ObservableCollection<AdminHourSlotModel>();
            pageTitle = string.Empty;
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
        /// Loads the hour bookings from Firestore.
        /// </summary>
        private async void Load()
        {
            hours.Clear();
            string clubName = Preferences.Get(Keys.AdminSelectedClub, string.Empty);
            string date = Preferences.Get(Keys.AdminSelectedDate, string.Empty);
            int court = Preferences.Get(Keys.AdminSelectedCourt, 0);
            pageTitle = $"{Strings.CourtPrefix} {court} - {date}";
            OnPropertyChanged(nameof(PageTitle));
            if (clubName != string.Empty && date != string.Empty && court != 0)
            {
                IDocumentReference document = fbData.fs.Collection(clubName).Document(court + "_" + date);
                IDocumentSnapshot snapshot = await document.GetAsync();
                if (snapshot.Exists && snapshot.Data != null)
                {
                    IList<Client>? clientsList = snapshot.Get<IList<Client>>(Keys.LclientsField);
                    if (clientsList != null)
                        for (int i = 0; i < clientsList.Count; i++)
                            hours.Add(new AdminHourSlot(i, clientsList[i].Name));
                }
            }
        }
        /// <summary>
        /// Navigates back to the courts page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistsCourts?refresh=true");
        }
        #endregion
    }
}
