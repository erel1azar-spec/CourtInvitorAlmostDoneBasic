using CourtInvitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.ModelsLogic
{
    internal class ClientExistingDatesList
    {
        private readonly FbData fbData = new();
        private readonly ClientExistingClubList clientExistingClubList = new();

        // פונקציה שמחזירה את כל התאריכים של המועדון
        public async Task<List<ClientExistingDatesListModel>> LoadClieDatesAsync()
        {
            // קבלת שם המועדון של המשתמש הנוכחי
            string clubName = Preferences.Get(Keys.SelectedClientClub, string.Empty);

            if (string.IsNullOrEmpty(clubName))
                return new List<ClientExistingDatesListModel>();

            // שליפת כל המסמכים של המועדון
            var snapshot = await fbData.fs.Collection(clubName).GetAsync();

            var dateModels = new List<ClientExistingDatesListModel>();

            foreach (var doc in snapshot.Documents)
            {
                if (doc.Data.ContainsKey("date"))
                {
                    string date = doc.Data["date"]?.ToString() ?? "";

                    // נוודא שהתאריך לא נוסף כבר
                    bool exists = false;
                    foreach (var model in dateModels)
                    {
                        if (model.DateText == date)
                        {
                            exists = true;
                            break;
                        }
                    }

                    if (!exists && !string.IsNullOrEmpty(date))
                    {
                        var model = new ClientExistingDatesListModel();
                        model.DateText = date;
                        dateModels.Add(model);
                    }
                }
            }

            return dateModels;
        }
    }
}
