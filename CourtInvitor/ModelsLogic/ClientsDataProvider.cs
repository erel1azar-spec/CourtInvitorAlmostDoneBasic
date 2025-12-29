using CourtInvitor.Models;
using System.Collections.ObjectModel;

namespace CourtInvitor.ModelsLogic
{
    public static class ClientsDataProvider
    {
        public static ObservableCollection<Client> LoadClients()
        {
            ObservableCollection<Client> clients =new ObservableCollection<Client>();

            for (int i = 0; i < 17; i++)
                clients.Add(new Client());

            return clients;
        }
    }
}
