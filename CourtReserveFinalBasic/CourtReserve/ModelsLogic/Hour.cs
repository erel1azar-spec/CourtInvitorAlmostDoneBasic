using CourtReserve.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class Hour : HourModel
    {
        #region Fields
        private readonly FbData data = new FbData();
        #endregion
        #region Public Functions
        public override void LoadFreeHours()
        {
            _ = LoadFreeHoursAsync();
        }
        public override void SelectHour(int hourIndex)
        {
            _ = SelectHourInternalAsync(hourIndex);
        }
        #endregion
        #region Protected Functions
        protected override async Task LoadFreeHoursAsync()
        {
            await LoadClientsFromFirestoreAsync();
            freeHours.Clear();
            for (int i = 0; i < clients.Count; i++)
                if (IsHourFree(clients[i]))
                    freeHours.Add(new HourText(i));
        }
        protected override async Task LoadClientsFromFirestoreAsync()
        {
            clients.Clear();
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
            string date = Preferences.Get(Keys.ClientSelectedDate, string.Empty);
            int court = Preferences.Get(Keys.ClientSelectedCourt, 0);
            if (clubName != string.Empty && date != string.Empty && court != 0)
            {
                IDocumentReference document = data.fs.Collection(clubName).Document(court + "_" + date);
                IDocumentSnapshot snapshot = await document.GetAsync();
                if (snapshot.Exists && snapshot.Data != null)
                {
                    IList<Client>? clientsList = snapshot.Get<IList<Client>>(Keys.LclientsField);
                    if (clientsList != null)
                        foreach (Client client in clientsList)
                            clients.Add(client);
                }
            }
        }
        protected override async Task SelectHourInternalAsync(int hourIndex)
        {
            bool saved = await SaveClientToSpecificHourAsync(hourIndex);
            if (saved)
            {
                clients[hourIndex].UserId = Preferences.Get(Keys.UserIdKey, string.Empty);
                clients[hourIndex].Name = Preferences.Get(Keys.UserNameKey, string.Empty);
                await LoadFreeHoursAsync();
            }
        }
        protected override bool IsHourFree(Client client)
        {
            return client.UserId == string.Empty && client.Name == string.Empty;
        }
        protected override async Task<bool> SaveClientToSpecificHourAsync(int index)
        {
            bool success = false;
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
            string date = Preferences.Get(Keys.ClientSelectedDate, string.Empty);
            int court = Preferences.Get(Keys.ClientSelectedCourt, 0);
            string userName = Preferences.Get(Keys.UserNameKey, string.Empty);
            string userId = Preferences.Get(Keys.UserIdKey, string.Empty);
            if (clubName != string.Empty && date != string.Empty && court != 0)
            {
                IDocumentReference document = data.fs.Collection(clubName).Document(court + "_" + date);
                IDocumentSnapshot snapshot = await document.GetAsync();
                if (snapshot.Exists && snapshot.Data != null)
                {
                    IList<Client>? clientsList = snapshot.Get<IList<Client>>(Keys.LclientsField);
                    if (clientsList != null && index >= 0 && index < clientsList.Count && IsHourFree(clientsList[index]))
                    {
                        clientsList[index].UserId = userId;
                        clientsList[index].Name = userName;
                        Dictionary<string, object> update = new Dictionary<string, object>
                        {
                            { Keys.LclientsField, clientsList }
                        };
                        await document.UpdateAsync(update);
                        success = true;
                    }
                }
            }
            return success;
        }
        #endregion
        #region Public Functions
        public override async Task AdminLoadAsync()
        {
            hours.Clear();
            string clubName = Preferences.Get(Keys.AdminSelectedClub, string.Empty);
            string date = Preferences.Get(Keys.AdminSelectedDate, string.Empty);
            int court = Preferences.Get(Keys.AdminSelectedCourt, 0);
            pageTitle = $"{Strings.CourtPrefix} {court} - {date}";
            if (clubName != string.Empty && date != string.Empty && court != 0)
            {
                IDocumentReference document = data.fs.Collection(clubName).Document(court + "_" + date);
                IDocumentSnapshot snapshot = await document.GetAsync();
                if (snapshot.Exists && snapshot.Data != null)
                {
                    IList<Client>? clientsList = snapshot.Get<IList<Client>>(Keys.LclientsField);
                    if (clientsList != null)
                        for (int i = 0; i < clientsList.Count; i++)
                            hours.Add(new AdminHourText(i, clientsList[i].Name));
                }
            }
        }
        #endregion

    }
}

