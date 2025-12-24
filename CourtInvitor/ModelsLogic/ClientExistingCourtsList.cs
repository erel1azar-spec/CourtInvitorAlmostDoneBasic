using CourtInvitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.ModelsLogic
{
    internal class ClientExistingCourtsList
    {
        private readonly FbData fbData = new();

        // פונקציה שמחזירה את כל התאריכים של המועדון


        public async Task<int> GetClientClubNumCourtsForCurrentUserAsync()
        {
            string clubName = Preferences.Get(Keys.SelectedClientClub, string.Empty);

            var snapshot = await fbData.fs.Collection("clubs").GetAsync();

            foreach (var doc in snapshot.Documents)
            {
                string clubUserName = doc.Data["name"]?.ToString() ?? "";
                if (clubUserName == clubName)
                {
                    int courtsCount = 0;
                    int.TryParse(doc.Data["courtsCount"]?.ToString() ?? "0", out courtsCount);
                    return courtsCount;
                }
            }

            return 0;
        }
    }
}
