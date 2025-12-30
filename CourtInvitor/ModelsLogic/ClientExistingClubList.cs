using CourtInvitor.Models;
using Plugin.CloudFirestore;

namespace CourtInvitor.ModelsLogic
{
    internal class ClientExistingClubList:ClientExistingClubListModel
    {
        private readonly FbData fbData;
        private string clubText;

        public override string ClubText => clubText;

        public ClientExistingClubList()
        {
            fbData = new FbData();
            clubText = string.Empty;
        }
        public static async Task<List<ClientExistingClubListModel>> LoadClientClubAsync()
        {
            List<ClientExistingClubListModel> clubs = new();


            FbData data = new FbData();

            IQuerySnapshot snapshot =await data.fs.Collection(ConstData.Clubs).GetAsync();

            foreach (IDocumentSnapshot document in snapshot.Documents)
            {
                if (document.Data != null && document.Data.ContainsKey(Keys.ClubName))
                {

                    string club = document.Get<string>(Keys.ClubName) ?? string.Empty;

                    if (club != string.Empty)
                    {

                        bool exists = clubs.Any(d => d.ClubText == club);

                        if (!exists)
                        {

                            ClientExistingClubList model = new ClientExistingClubList();

                            model.clubText = club;
                            clubs.Add(model);
                        }
                    }
                }
            }

            return clubs;
        }
    }
    
}
