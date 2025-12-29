using CourtInvitor.Models;
using Plugin.CloudFirestore;

namespace CourtInvitor.ModelsLogic
{
    internal class ClientExistingDatesList:ClientExistingDatesListModel
    {
        private readonly FbData fbData;
        private string dateTextClient;

        public override string DateTextClient => dateTextClient;

        public ClientExistingDatesList()
        {
            fbData = new FbData();
            dateTextClient = string.Empty;
        }
        public static async Task<List<ClientExistingDatesListModel>>  LoadClientDatesAsync(string clubName)
        {

            List<ClientExistingDatesListModel> dates = new List<ClientExistingDatesListModel>();
            if (clubName == string.Empty)
                return dates;

            FbData data = new FbData();

            IQuerySnapshot snapshot =await data.fs.Collection(clubName).GetAsync();

            foreach (IDocumentSnapshot document in snapshot.Documents)
            {
                    if (document.Data != null && document.Data.ContainsKey(Keys.Date))
                    {
                        string? date = document.Get<string>(Keys.Date);

                        if (date != string.Empty)
                        {
                            bool exists = dates.Any(d => d.DateTextClient == date);

                            if (!exists)
                            {
                                ClientExistingDatesList model = new ClientExistingDatesList();

                                if(date != null)
                                {
                                model.dateTextClient = date;
                                }
                                dates.Add(model);
                            }
                        }
                    }
                
            }
            return dates;
        }
    }
    
}
