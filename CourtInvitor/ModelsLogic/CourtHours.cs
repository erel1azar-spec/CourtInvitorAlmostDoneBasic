using CourtInvitor.Models;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of court hours logic.
    /// </summary>
    internal class CourtHours : CourtHoursModel
    {
        #region Fields
        private readonly FbData data;
        private readonly ObservableCollection<Client> clients;
        private readonly ObservableCollection<HourSlotModel> freeHours;
        #endregion
        #region Properties
        public override ObservableCollection<Client> Clients => clients;
        public override ObservableCollection<HourSlotModel> FreeHours => freeHours;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CourtHours class.
        /// </summary>
        public CourtHours()
        {
            data = new FbData();
            clients = new ObservableCollection<Client>();
            freeHours = new ObservableCollection<HourSlotModel>();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Loads free hours from Firestore.
        /// </summary>
        public override void LoadFreeHours()
        {
            _ = LoadFreeHoursAsync();
        }
        /// <summary>
        /// Selects and books the specified hour.
        /// </summary>
        /// <param name="hourIndex">The index of the hour to book.</param>
        public override void SelectHour(int hourIndex)
        {
            _ = SelectHourInternalAsync(hourIndex);
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Loads free hours from Firestore asynchronously.
        /// </summary>
        private async Task LoadFreeHoursAsync()
        {
            await LoadClientsFromFirestoreAsync();
            freeHours.Clear();
            for (int i = 0; i < clients.Count; i++)
                if (IsHourFree(clients[i]))
                    freeHours.Add(new HourSlot(i));
        }
        /// <summary>
        /// Loads client data from Firestore.
        /// </summary>
        private async Task LoadClientsFromFirestoreAsync()
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
        /// <summary>
        /// Selects and books the hour asynchronously.
        /// </summary>
        /// <param name="hourIndex">The hour index to book.</param>
        private async Task SelectHourInternalAsync(int hourIndex)
        {
            bool saved = await SaveClientToSpecificHourAsync(hourIndex);
            if (saved)
            {
                clients[hourIndex].UserId = Preferences.Get(Keys.UserIdKey, string.Empty);
                clients[hourIndex].Name = Preferences.Get(Keys.UserNameKey, string.Empty);
                await LoadFreeHoursAsync();
            }
        }
        /// <summary>
        /// Checks if the hour slot is free.
        /// </summary>
        /// <param name="client">The client data for the hour.</param>
        /// <returns>True if hour is free.</returns>
        private static bool IsHourFree(Client client)
        {
            return client.UserId == string.Empty && client.Name == string.Empty;
        }
        /// <summary>
        /// Saves the client booking to Firestore.
        /// </summary>
        /// <param name="index">The hour index to book.</param>
        /// <returns>True if booking succeeded.</returns>
        private async Task<bool> SaveClientToSpecificHourAsync(int index)
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
    }
}
