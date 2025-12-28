using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
{
    public static class ClientsDataProvider
    {
        public static ObservableCollection<Client> LoadClients()
        {
            ObservableCollection<Client> clients =
                new ObservableCollection<Client>();

            for (int i = 0; i < 17; i++)
                clients.Add(new Client());

            return clients;
        }
    }
}
