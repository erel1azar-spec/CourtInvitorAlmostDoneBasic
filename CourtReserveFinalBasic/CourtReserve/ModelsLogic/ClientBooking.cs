using CourtReserve.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class ClientBooking : ClientBookingModel
    {
        #region Fields
        private readonly FbData fbData = new FbData();
        #endregion

        #region Constructor
        public ClientBooking() { }
        #endregion

        #region Public Functions
        public override async Task LoadAsync()
        {
            bookings.Clear();

            string username = Preferences.Default.Get(Keys.UserNameKey, string.Empty);
            if (string.IsNullOrWhiteSpace(username))
                return;

            IQuerySnapshot clubsSnapshot = await fbData.fs.Collection(Keys.CollectionClubName).GetAsync();
            if (clubsSnapshot == null || clubsSnapshot.Count == 0)
                return;

            foreach (IDocumentSnapshot clubDoc in clubsSnapshot.Documents)
            {
                string? clubName = clubDoc.Get<string>(Keys.CollectionClubName);
                await ProcessClubBookings(clubName, username);
            }
        }
        #endregion

        #region Private Functions
        private async Task ProcessClubBookings(string clubName, string targetUsername)
        {
            IQuerySnapshot datesSnapshot = await fbData.fs.Collection(clubName).GetAsync();

            foreach (IDocumentSnapshot dateDoc in datesSnapshot.Documents)
            {
                if (!dateDoc.Exists || dateDoc.Data == null)
                    continue;

                if (!dateDoc.Data.TryGetValue("Lclinet", out object rawList) || rawList == null)
                    continue;

                if (rawList is not IList<object> clientList)
                    continue;

                int courtNumber = 0;
                if (dateDoc.Data.TryGetValue("courtnumber", out object courtRaw))
                    int.TryParse(courtRaw?.ToString(), out courtNumber);

                string dateStr = dateDoc.Data.TryGetValue("date", out object dateValue)
                    ? dateValue?.ToString() ?? dateDoc.Id
                    : dateDoc.Id;

                for (int hourIndex = 0; hourIndex < clientList.Count; hourIndex++)
                {
                    object slot = clientList[hourIndex];
                    if (slot is not IDictionary<string, object> slotDict)
                        continue;

                    if (!slotDict.TryGetValue("username", out object usernameObj) || usernameObj == null)
                        continue;

                    string slotUsername = usernameObj.ToString();
                    if (string.Equals(slotUsername, targetUsername, StringComparison.OrdinalIgnoreCase))
                        AddBooking(clubName, dateStr, courtNumber, hourIndex);
                }
            }
        }
        #endregion

        #region Protected Functions
        protected override void AddBooking(string clubName, string date, int courtNumber, int hourIndex)
        {
            bookings.Add(new ClientBookingText(clubName, date, courtNumber, hourIndex));
        }
        #endregion
    }
} 

