using CourtReserve.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class Court:CourtModel
    {
        #region Fields
        private readonly FbData fbData=new FbData();
        #endregion
        #region Public Functions
        public override async Task LoadAsync()
        {
            courts.Clear();
            string clubName = Preferences.Get(Keys.AdminSelectedClub, string.Empty);
            string date = Preferences.Get(Keys.AdminSelectedDate, string.Empty);
            if (clubName != string.Empty && date != string.Empty)
            {
                IQuerySnapshot snapshot = await fbData.fs.Collection(clubName).WhereEqualsTo(Keys.Date, date).GetAsync();
                foreach (IDocumentSnapshot document in snapshot.Documents)
                    AddCourtFromDocument(document);
            }
        }
        public override async Task LoadAsyncClient()
        {
            courts.Clear();
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
            string date = Preferences.Get(Keys.ClientSelectedDate, string.Empty);
            if (clubName != string.Empty && date != string.Empty)
            {
                IQuerySnapshot snapshot = await fbData.fs.Collection(clubName).WhereEqualsTo(Keys.Date, date).GetAsync();
                foreach (IDocumentSnapshot document in snapshot.Documents)
                    AddCourtFromDocument(document);
            }
        }
        public override void SelectCourt(AdminExistingCourtsTextModel court)
        {
            if (court != null)
                Preferences.Set(Keys.AdminSelectedCourt, court.CourtNumber);
        }
        public override void SelectCourtClient(AdminExistingCourtsTextModel court)
        {
            if (court != null)
                Preferences.Set(Keys.ClientSelectedCourt, court.CourtNumber);
        }
        #endregion
        #region Protected Functions
        protected override void AddCourtFromDocument(IDocumentSnapshot document)
        {
            if (document.Data != null && document.Data.ContainsKey(Keys.CourtNumber))
            {
                int number = document.Get<int>(Keys.CourtNumber);
                if (number > 0 && !courts.Any(c => c.CourtNumber == number))
                {
                    int totalSlots = 0;
                    int freeSlots = 0;

                    if (document.Data.ContainsKey(Keys.LclientsField))
                    {
                        IList<Client>? clientsList = document.Get<IList<Client>>(Keys.LclientsField);
                        if (clientsList != null)
                        {
                            totalSlots = clientsList.Count;
                            foreach (Client c in clientsList)
                            {
                                if (c.UserId == string.Empty && c.Name == string.Empty)
                                    freeSlots++;
                            }
                        }
                    }

                    courts.Add(new AdminExistingCourtsText(number, freeSlots, totalSlots));
                }
            }
        }
        #endregion
    }
}
