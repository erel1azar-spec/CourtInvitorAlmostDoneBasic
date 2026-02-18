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
    public class ClientExistingHoursPageVM:ObservableObject,IQueryAttributable
    {
        #region Fields
        private readonly HourModel model;
        #endregion
        #region Properties
        public ObservableCollection<HourTextModel> FreeHours => model.FreeHours;
        #endregion
        #region Commands
        public ICommand HourSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        public ClientExistingHoursPageVM()
        {
            model = new Hour();
            HourSelectedCommand = new Command<int>(SelectHour);
            NavBackHomeCommand = new Command(NavHome);
            model.LoadFreeHours();
        }
        #endregion
        #region Public Functions
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("refresh"))
                model.LoadFreeHours();
        }
        #endregion
        #region Private Functions
        private void SelectHour(int index)
        {
            model.SelectHour(index);
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///ClientExistingCourtsPage?refresh=true");
        }
        #endregion
    }
}
