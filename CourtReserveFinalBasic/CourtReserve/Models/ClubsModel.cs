using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class ClubsModel
    {
        protected abstract Task LoadByUserEmailAsync(string email);
        public abstract Task LoadAsyncClient();
        public abstract Task FilterClubsByAvailabilityAsync(string date, int hourIndex);
        protected abstract Task<IDocumentSnapshot?> FindClubByEmailAsync(string email);
        protected abstract void ExtractClubData(IDocumentSnapshot document, string email);
        protected ObservableCollection<AdminExistingClubsTextModel> clubs = new();
        public ObservableCollection<AdminExistingClubsTextModel> Clubs => clubs;
        protected string name = string.Empty;
        protected string userEmail = string.Empty;
        public string Name => name;
        public string UserEmail => userEmail;
        public abstract Task LoadAsync();
        public abstract void SelectClub(string club);
        public abstract void SelectClubClient(string club);
        public abstract Task EnsureAllClubsHaveDocumentsAsync();
        protected abstract void AddClubFromDocument(IDocumentSnapshot document);
        public abstract List<string> GetDateOptions();
        public abstract List<string> GetHourOptions();
        public abstract int HourTextToIndex(string hourText);
    }
}
