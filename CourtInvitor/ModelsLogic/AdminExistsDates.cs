using CourtInvitor.Models;
using Plugin.CloudFirestore;

namespace CourtInvitor.ModelsLogic
{
    public class AdminExistsDates:AdminExistsDatesModel
    {
        private readonly FbData fbData;
        private string dateText;

        public override string DateText => dateText;

        public AdminExistsDates()
        {
            fbData = new FbData();
            dateText = string.Empty;
        }
        public static async Task<List<AdminExistsDatesModel>> LoadDatesAsync(string clubName)
        {
            List<AdminExistsDatesModel> dates = new List<AdminExistsDatesModel>();

            if (clubName == string.Empty)
                return dates;

            FbData data = new FbData();

            IQuerySnapshot snapshot =await data.fs.Collection(clubName).GetAsync();

            foreach (IDocumentSnapshot document in snapshot.Documents)
            {
                if (document.Data!=null&&document.Data.ContainsKey(Keys.Date))
                {

                    string ?date = document.Get<string>(Keys.Date);

                    if (date != string.Empty)
                    {
                        bool exists = dates.Any(d => d.DateText == date);

                        if (!exists)
                        {
                        

                            AdminExistsDates model = new AdminExistsDates();

                            if (date != null)
                            {
                                model.dateText = date;
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
