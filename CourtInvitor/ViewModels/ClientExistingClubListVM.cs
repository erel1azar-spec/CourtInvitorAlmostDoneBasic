using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the client existing club list page.
    /// </summary>
    internal class ClientExistingClubListVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly ObservableCollection<ClientExistingClubListModel> clubs;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of clubs.
        /// </summary>
        public ObservableCollection<ClientExistingClubListModel> Clubs => clubs;
        #endregion
        #region Commands
        public ICommand ClubSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ClientExistingClubListVM class.
        /// </summary>
        public ClientExistingClubListVM()
        {
            clubs = new ObservableCollection<ClientExistingClubListModel>();
            NavBackHomeCommand = new Command(NavHome);
            ClubSelectedCommand = new Command<string>(OnClubSelected);
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
        /// Loads clubs from Firestore.
        /// </summary>
        private async void Load()
        {
            List<ClientExistingClubListModel> result = await ClientExistingClubList.LoadClientClubAsync();
            clubs.Clear();
            foreach (ClientExistingClubListModel model in result)
                clubs.Add(model);
        }
        /// <summary>
        /// Handles club selection.
        /// </summary>
        /// <param name="selectedClub">The selected club name.</param>
        private void OnClubSelected(string selectedClub)
        {
            Preferences.Set(Keys.ClientSelectedClub, selectedClub);
            Shell.Current.GoToAsync("///ClientExistingDatesList?refresh=true");
        }
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
