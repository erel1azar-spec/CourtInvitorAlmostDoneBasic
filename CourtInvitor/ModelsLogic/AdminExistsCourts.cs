using CourtInvitor.Models;
using Plugin.CloudFirestore;

namespace CourtInvitor.ModelsLogic
{
    internal class AdminExistsCourts:AdminExistsCourtsModel
    {
        private int courtNumber;
        private string courtText=string.Empty;

        public override int CourtNumber => courtNumber;

        public override string CourtText => courtText;

        public static async Task<List<AdminExistsCourtsModel>> LoadCourtsAsync(string clubName)
        {
            List<AdminExistsCourtsModel> courts = new();

            if (string.IsNullOrEmpty(clubName))
                return courts;

            FbData data = new FbData();

            IQuerySnapshot snapshot =await data.fs.Collection(clubName).GetAsync();

            foreach (IDocumentSnapshot document in snapshot.Documents)
            {
                if (document.Data != null && document.Data.ContainsKey(Keys.CourtNumber))
                {
                    int number = document.Get<int>(Keys.CourtNumber);
                    if (number > 0)
                    {

                        bool exists = courts.Any(c => c.CourtNumber == number);
                        if (!exists)
                        {


                            AdminExistsCourts model = new()
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

        //private readonly FbData fbData = new();

        //// פונקציה שמחזירה את כל התאריכים של המועדון


        //   public async Task<int> GetClubNumCourtsForCurrentUserAsync()
        //    {
        //        string userEmail = Preferences.Get(Keys.EmailKey, string.Empty);
        //        if (string.IsNullOrEmpty(userEmail))
        //         return 0;

        //     var snapshot = await fbData.fs.Collection("clubs").GetAsync();

        //        foreach (var doc in snapshot.Documents)
        //        {
        //            string clubUserEmail = doc.Data["userEmail"]?.ToString() ?? "";
        //            if (clubUserEmail == userEmail)
        //            {
        //                int courtsCount = 0;
        //                int.TryParse(doc.Data["courtsCount"]?.ToString() ?? "0", out courtsCount);
        //                return courtsCount;
        //            }
        //        }

        //    return 0;
        //}
    }
    
}
