using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class HourModel
    {

        #region Fields
        protected ObservableCollection<AdminHourTextModel> hours = new();
        protected string pageTitle = string.Empty;
        #endregion
        #region Properties
        public ObservableCollection<AdminHourTextModel> Hours => hours;
        public string PageTitle => pageTitle;
        #endregion
        #region Public Functions
        public abstract Task AdminLoadAsync();
        #endregion




        #region Fields
        protected ObservableCollection<Client> clients = new();
        protected ObservableCollection<HourTextModel> freeHours = new();
        #endregion
        #region Properties
        public ObservableCollection<Client> Clients => clients;
        public ObservableCollection<HourTextModel> FreeHours => freeHours;
        #endregion
        #region Public Functions
        public abstract void LoadFreeHours();
        public abstract void SelectHour(int index);
        #endregion
        #region Protected Functions
        protected abstract Task LoadFreeHoursAsync();
        protected abstract Task LoadClientsFromFirestoreAsync();
        protected abstract Task SelectHourInternalAsync(int hourIndex);
        protected abstract bool IsHourFree(Client client);
        protected abstract Task<bool> SaveClientToSpecificHourAsync(int index);
        #endregion
    }
}
