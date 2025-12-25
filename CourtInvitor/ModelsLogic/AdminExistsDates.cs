//using CourtInvitor.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CourtInvitor.ModelsLogic
//{
//    public class AdminExistsDates
//    {
//        private readonly FbData fbData = new();
//        private readonly AdminExistsClubs adminExistsClubs = new();
//        string email = Preferences.Get(Keys.EmailKey, string.Empty);

//        // פונקציה שמחזירה את כל התאריכים של המועדון
//        public async Task<List<AdminExistsDatesModel>> LoadDatesAsync()
//        {
//            // קבלת שם המועדון של המשתמש הנוכחי
//            string clubName = await adminExistsClubs.LoadByUserEmailAsync(email);

//            if (string.IsNullOrEmpty(clubName))
//                return new List<AdminExistsDatesModel>();

//            // שליפת כל המסמכים של המועדון
//            var snapshot = await fbData.fs.Collection(clubName).GetAsync();

//            var dateModels = new List<AdminExistsDatesModel>();

//            foreach (var doc in snapshot.Documents)
//            {
//                if (doc.Data.ContainsKey("date"))
//                {
//                    string date = doc.Data["date"]?.ToString() ?? "";

//                    // נוודא שהתאריך לא נוסף כבר
//                    bool exists = false;
//                    foreach (var model in dateModels)
//                    {
//                        if (model.DateText == date)
//                        {
//                            exists = true;
//                            break;
//                        }
//                    }

//                    if (!exists && !string.IsNullOrEmpty(date))
//                    {
//                        var model = new AdminExistsDatesModel();
//                        model.DateText = date;
//                        dateModels.Add(model);
//                    }
//                }
//            }

//            return dateModels;


//        }
//    }
//}
