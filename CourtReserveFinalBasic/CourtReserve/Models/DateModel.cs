using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class DateModel
    {
        protected ObservableCollection<AdminExistingDatesTextModel> dates = new();
        public ObservableCollection<AdminExistingDatesTextModel> Dates => dates;
        public abstract Task LoadAsync();
        public abstract Task LoadAsyncClient();
        public abstract void SelectDate(string date);
        public abstract void SelectDateClient(string date);
        protected abstract void AddDateFromDocument(IDocumentSnapshot document);
    }
}
