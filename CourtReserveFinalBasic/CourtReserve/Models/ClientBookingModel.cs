using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class ClientBookingModel
    {
        protected ObservableCollection<ClientBookingTextModel> bookings = new();
        public ObservableCollection<ClientBookingTextModel> Bookings => bookings;

        public abstract Task LoadAsync();
        protected abstract void AddBooking( string clubName,string date,int courtNumber, int hourIndex);
    }
}
