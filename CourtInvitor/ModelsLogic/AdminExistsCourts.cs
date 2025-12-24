using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
{
    internal class AdminExistsCourts
    {
        private readonly FbData fbData = new();

        // פונקציה שמחזירה את כל התאריכים של המועדון


           public async Task<int> GetClubNumCourtsForCurrentUserAsync()
            {
                string userEmail = Preferences.Get(Keys.EmailKey, string.Empty);
                if (string.IsNullOrEmpty(userEmail))
                 return 0;

             var snapshot = await fbData.fs.Collection("clubs").GetAsync();

                foreach (var doc in snapshot.Documents)
                {
                    string clubUserEmail = doc.Data["userEmail"]?.ToString() ?? "";
                    if (clubUserEmail == userEmail)
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
