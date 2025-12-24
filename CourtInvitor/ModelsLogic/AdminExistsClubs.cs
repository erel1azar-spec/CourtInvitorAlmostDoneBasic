using CourtInvitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
{
    internal class AdminExistsClubs
    {
        private readonly FbData fbData = new();

        public async Task<string> GetClubNameForCurrentUserAsync()
        {

            string userEmail =
                Preferences.Default.Get(Keys.EmailKey, string.Empty);


            if (userEmail == string.Empty)
                return string.Empty;

            var snapshot = await fbData.fs
                .Collection("clubs")
                .GetAsync();

            foreach (var document in snapshot.Documents)
            {
                string? clubUserEmail = document.Data["userEmail"].ToString();

                if (clubUserEmail == userEmail)
                {
                    return document.Data["name"].ToString();
                }
            }

            return string.Empty;
        }

    }
}
