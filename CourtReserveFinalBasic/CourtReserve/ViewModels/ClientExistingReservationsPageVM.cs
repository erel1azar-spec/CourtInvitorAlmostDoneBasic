
using CommunityToolkit.Mvvm.Input;
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
    internal class ClientExistingReservationsPageVM:ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly ClientBookingModel model = new ClientBooking();
        #endregion

        #region Properties

        public ObservableCollection<ClientBookingTextModel> Bookings => model.Bookings;
        #endregion

        #region Commands
        public IAsyncRelayCommand NavBackCommand { get; }
        #endregion

        #region Constructor
        public ClientExistingReservationsPageVM()
        {
            NavBackCommand = new AsyncRelayCommand(NavBackAsync);
        }
        #endregion

        #region Public Functions
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _ = LoadBookingsAsync();
        }
        #endregion

        #region Private Functions
        private async Task NavBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
        private async Task LoadBookingsAsync()
        { 
            await model.LoadAsync();
        }
        #endregion
    }
}
