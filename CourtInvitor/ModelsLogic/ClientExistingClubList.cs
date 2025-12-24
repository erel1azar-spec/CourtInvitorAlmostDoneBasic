using CourtInvitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.ModelsLogic
{
    internal class ClientExistingClubList
    {
        private readonly FbData fbData = new();
        private readonly AdminExistsClubs adminExistsClubs = new();

        // פונקציה שמחזירה את כל התאריכים של המועדון
        public async Task<List<ClientExistingClubListModel>> LoadClubNamesAsync()
        {

            // שליפת כל המסמכים של המועדון
            var snapshot = await fbData.fs
    .Collection("clubs")
    .GetAsync();

            var dateModels = new List<ClientExistingClubListModel>();

            foreach (var doc in snapshot.Documents)
            {
                if (doc.Data.ContainsKey("name"))
                {
                    string name = doc.Data["name"]?.ToString() ?? "";

                    // נוודא שהתאריך לא נוסף כבר


                    if (string.IsNullOrEmpty(name)==false)
                    {
                        var model = new ClientExistingClubListModel();
                        model.ClubText = name;
                        dateModels.Add(model);
                    }
                }
            }

            return dateModels;


        }
    }
}
