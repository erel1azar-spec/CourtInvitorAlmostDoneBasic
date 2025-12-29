using CourtInvitor.Models;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;

namespace CourtInvitor.ModelsLogic
{
    internal class CourtHours:CourtHoursModel
    {
        private readonly FbData data;
        private readonly ObservableCollection<Client> clients;
        private readonly ObservableCollection<HourSlotModel> freeHours;

        public override ObservableCollection<Client> Clients => clients;
        public override ObservableCollection<HourSlotModel> FreeHours => freeHours;

        public CourtHours()
        {
            data = new FbData();
            clients = ClientsDataProvider.LoadClients();
            freeHours = new ObservableCollection<HourSlotModel>();
        }

        public override void LoadFreeHours()
        {
            freeHours.Clear();

            for (int i = 0; i < clients.Count; i++)
                if (IsHourFree(clients[i]))
                    freeHours.Add(new HourSlot(i));
        }
        public override void SelectHour(int hourIndex)
        {
            _ =SelectHourInternalAsync(hourIndex);
        }

        private async Task SelectHourInternalAsync(int hourIndex)
        {
            bool saved = await SaveClientToSpecificHourAsync(hourIndex);
            if (!saved)
                return;

            clients[hourIndex].UserId =
                Preferences.Get(Keys.UserIdKey, string.Empty);

            clients[hourIndex].Name =
                Preferences.Get(Keys.UserNameKey, string.Empty);

            LoadFreeHours();
        }

        private static bool IsHourFree(Client client)
        {
            return client.UserId == string.Empty && client.Name == string.Empty;
        }

        private async Task<bool> SaveClientToSpecificHourAsync(int index)
        {
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);

            string date = Preferences.Get(Keys.ClientSelectedDate, string.Empty);

            int court = Preferences.Get(Keys.ClientSelectedCourt,0);

            string userName = Preferences.Get(Keys.UserNameKey, string.Empty);

            string userId = Preferences.Get(Keys.UserIdKey, string.Empty);

            if (clubName == string.Empty || date == string.Empty || court == 0)
                return false;

            IDocumentReference document = data.fs.Collection(clubName).Document(court + "_" + date);

            IDocumentSnapshot snapshot = await document.GetAsync();

            if (!snapshot.Exists || snapshot.Data == null)
                return false;

            IList<Client>? clientsList = snapshot.Get<IList<Client>>("Lclients") ?? new List<Client>();

            if (clientsList == null || index < 0 ||index >= clientsList.Count)
                return false;

            if (!IsHourFree(clientsList[index]))
                return false;

            clientsList[index].UserId = userId;
            clientsList[index].Name = userName;

            Dictionary<string, object> update =
                new Dictionary<string, object>
                {
                    { "Lclients", clientsList }
                };

            await document.UpdateAsync(update);
            return true;
        }
        //private readonly FbData data = new FbData();
        //private readonly ObservableCollection<Client> clients;
        //private readonly ObservableCollection<HourSlotModel> freeHours;

        //public override ObservableCollection<Client> Clients => clients;

        //public override ObservableCollection<HourSlotModel> FreeHours => freeHours;

        //public CourtHours()
        //{
        //    clients = ClientsDataProvider.LoadClients();
        //    freeHours = new ObservableCollection<HourSlotModel>();
        //}

        //public override void LoadFreeHours()
        //{
        //    freeHours.Clear();

        //    for (int i = 0; i < clients.Count; i++)
        //        if (IsHourFree(clients[i]))
        //            freeHours.Add(new HourSlot(i));
        //}

        //public override void SelectHour(int hourIndex)
        //{
        //    Client client = clients[hourIndex];
        //    client.UserId = Preferences.Get(Keys.UserIdKey,string.Empty);
        //    client.Name = Preferences.Get(Keys.UserNameKey, string.Empty);
        //}

        //private static bool IsHourFree(Client client)
        //{
        //    return client.UserId == string.Empty &&
        //           client.Name == string.Empty;
        //}
        //public async Task<bool> SaveClientToSpecificHourAsync(string clubName, int index, int courtNumber, DateTime date, int slotIndex, Client newClient)
        //{
        //    bool saved = false;
        //    string ClubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
        //    string Date = Preferences.Get(Keys.ClientSelectedDate, string.Empty);
        //    string Court = Preferences.Get(Keys.ClientSelectedCourt, string.Empty);
        //    string UserName = Preferences.Get(Keys.UserNameKey, string.Empty);
        //    string UserId = Preferences.Get(Keys.UserIdKey, string.Empty);
        //    IDocumentSnapshot document =
        //       await data.fs
        //       .Collection(ClubName).
        //       Document(Court + "_" + Date)
        //       .GetAsync();
        //    IList<Client> clientsList = new List<Client>();

        //    if (document.Exists && document.Data != null)
        //    {
        //        if (document.Data.ContainsKey("Lclients"))
        //        {
        //            clientsList =
        //                document.Get<IList<Client>>("Lclients");
        //        }
        //    }

        //    clientsList[index].Name = UserName;
        //    clientsList[index].UserId = UserId;


        //    object updatedDoc = new
        //    {
        //        date = Date,
        //        CourtNumber = courtNumber,
        //        Lclients = clientsList
        //    };
        //    return saved;
        //}
    }
}
