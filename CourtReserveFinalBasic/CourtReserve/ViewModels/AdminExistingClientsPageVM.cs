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
    public class AdminExistingClientsPageVM:ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly HourModel model;
        #endregion
        #region Properties
        public ObservableCollection<AdminHourTextModel> Hours => model.Hours;
        public string PageTitle => model.PageTitle;
        #endregion
        #region Commands
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        public AdminExistingClientsPageVM()
        {
            model = new Hour();
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
            await model.AdminLoadAsync();
            OnPropertyChanged(nameof(PageTitle));
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistingCourtsPage?refresh=true");
        }
        #endregion
    }
}
