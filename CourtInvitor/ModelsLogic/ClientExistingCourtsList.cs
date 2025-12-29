using CourtInvitor.Models;
using Plugin.CloudFirestore;

namespace CourtInvitor.ModelsLogic
{
    internal class ClientExistingCourtsList:ClientExistingCourtsListModel
    {
        private int courtNumber;
        private string courtText = string.Empty;

        public override int CourtNumber => courtNumber;

        public override string CourtText => courtText;

        public static async Task<List<ClientExistingCourtsListModel>> LoadCourtsAsync(string clubName)
        {
            List<ClientExistingCourtsListModel> courts = new();

            if (string.IsNullOrEmpty(clubName))
                return courts;

            FbData data = new();
            IQuerySnapshot snapshot =await data.fs.Collection(clubName).GetAsync();

            foreach (IDocumentSnapshot document in snapshot.Documents)
            {
                if (document.Data!=null&&document.Data.ContainsKey(Keys.CourtNumber))
                {
                    int number = document.Get<int>(Keys.CourtNumber);
                    if (number > 0)
                    {

                        bool exists = courts.Any(c => c.CourtNumber == number);
                        if (!exists)
                        {

                            ClientExistingCourtsList model = new()
                            {
                                courtNumber = number,
                                courtText = Strings.Court + number // "Court 1", "Court 2"...
                            };


                            courts.Add(model);
                        }
                    }
                }
            }

            return courts;
        }
    }
}
