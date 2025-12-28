using CourtInvitor.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// טעינת כל התאריכים של מועדון
        /// </summary>
        /// <param name="clubName">שם המועדון</param>
        /// <returns>רשימת תאריכים</returns>
        public static async Task<List<AdminExistsDatesModel>> LoadDatesAsync(string clubName)
        {
            List<AdminExistsDatesModel> dates =
                new List<AdminExistsDatesModel>();

            if (clubName == string.Empty)
                return dates;

            FbData data = new FbData();

            IQuerySnapshot snapshot =
                await data.fs
                .Collection(clubName)
                .GetAsync();

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
        //private readonly FbData fbData = new();
        //private readonly AdminExistsClubs adminExistsClubs = new();
        //string email = Preferences.Get(Keys.EmailKey, string.Empty);

        //// פונקציה שמחזירה את כל התאריכים של המועדון
        //public async Task<List<AdminExistsDatesModel>> LoadDatesAsync()
        //{
        //    // קבלת שם המועדון של המשתמש הנוכחי
        //    string clubName = await adminExistsClubs.LoadByUserEmailAsync(email);

        //    if (string.IsNullOrEmpty(clubName))
        //        return new List<AdminExistsDatesModel>();

        //    // שליפת כל המסמכים של המועדון
        //    var snapshot = await fbData.fs.Collection(clubName).GetAsync();

        //    var dateModels = new List<AdminExistsDatesModel>();

        //    foreach (var doc in snapshot.Documents)
        //    {
        //        if (doc.Data.ContainsKey("date"))
        //        {
        //            string date = doc.Data["date"]?.ToString() ?? "";

        //            // נוודא שהתאריך לא נוסף כבר
        //            bool exists = false;
        //            foreach (var model in dateModels)
        //            {
        //                if (model.DateText == date)
        //                {
        //                    exists = true;
        //                    break;
        //                }
        //            }

        //            if (!exists && !string.IsNullOrEmpty(date))
        //            {
        //                var model = new AdminExistsDatesModel();
        //                model.DateText = date;
        //                dateModels.Add(model);
        //            }
        //        }
        //    }

        //    return dateModels;


    }
    
}
