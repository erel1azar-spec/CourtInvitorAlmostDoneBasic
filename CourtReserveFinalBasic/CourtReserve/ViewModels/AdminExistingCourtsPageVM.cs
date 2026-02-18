using CourtReserve.Models;
using CourtReserve.ModelsLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtReserve.ViewModels
{
    public class AdminExistingCourtsPageVM:ObservableObject,IQueryAttributable
    {
        #region Fields
        private readonly CourtModel model;
        #endregion
        #region Properties
        public ObservableCollection<AdminExistingCourtsTextModel> Courts => model.Courts;
        #endregion
        #region Commands
        public ICommand CourtSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        public AdminExistingCourtsPageVM()
        {
            model = new Court();
            CourtSelectedCommand = new Command<AdminExistingCourtsTextModel>(OnCourtSelected);
            NavBackHomeCommand = new Command(NavHome);
        }
        #endregion
        #region Public Functions
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Load();
        }
        #endregion
        #region Private Functions
        private async void Load()
        {
            await model.LoadAsync();
        }
        private void OnCourtSelected(AdminExistingCourtsTextModel selectedCourt)
        {
            model.SelectCourt(selectedCourt);
            Shell.Current.GoToAsync("///AdminExistingClientsPage?refresh=true");
        }
        /// <summary>
        /// Navigates back to the dates page.
        /// </summary>
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistingDatesPage?refresh=true");
        }
        #endregion
    }
}
