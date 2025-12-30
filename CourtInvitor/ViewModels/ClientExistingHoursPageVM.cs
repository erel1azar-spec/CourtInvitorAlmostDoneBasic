using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the client existing hours page.
    /// </summary>
    internal class ClientExistingHoursPageVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly CourtHoursModel model;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of free hours.
        /// </summary>
        public ObservableCollection<HourSlotModel> FreeHours => model.FreeHours;
        #endregion
        #region Commands
        public ICommand HourSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ClientExistingHoursPageVM class.
        /// </summary>
        public ClientExistingHoursPageVM()
        {
            model = new CourtHours();
            HourSelectedCommand = new Command<int>(SelectHour);
            NavBackHomeCommand = new Command(NavHome);
            model.LoadFreeHours();
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
                model.LoadFreeHours();
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Selects the specified hour.
        /// </summary>
        /// <param name="index">The index of the hour to select.</param>
        private void SelectHour(int index)
        {
            model.SelectHour(index);
        }
        /// <summary>
        /// Navigates back to the courts list page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///ClientExistingCourtsList?refresh=true");
        }
        #endregion
    }
}
