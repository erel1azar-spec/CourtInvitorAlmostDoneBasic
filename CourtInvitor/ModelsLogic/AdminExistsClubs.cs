using CourtInvitor.Models;
using CourtInvitor.Views;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.ModelsLogic
{
    internal class AdminExistsClubs:AdminExistsClubsModel
    {
        private readonly FbData fbData;
        private string name;
        private string userEmail;

        public override string Name => name;
        public override string UserEmail => userEmail;

        public AdminExistsClubs()
        {
            fbData = new FbData();
            name = string.Empty;
            userEmail = string.Empty;
        }
        public async Task LoadByUserEmailAsync(string email)
        {
            IQuerySnapshot ?snapshot =
                await fbData.fs
                .Collection(ConstData.Clubs)
                .WhereEqualsTo(
                    Keys.UserEmail,
                    email)
                .GetAsync();

            IDocumentSnapshot ?document =
                snapshot.Documents.FirstOrDefault();

            if (document != null)
            {
                string? tempName = document.Get<string>(Keys.Name);
                string? tempEmail = document.Get<string>(Keys.UserEmail);

                if (tempName != null)
                    name = tempName;

                if (tempEmail != null)
                    userEmail = tempEmail;
            }
        }



        //private readonly FbData fbData = new();

        //public async Task<string> GetClubNameForCurrentUserAsync()
        //{

        //    string userEmail =
        //        Preferences.Default.Get(Keys.EmailKey, string.Empty);


        //    if (userEmail == string.Empty)
        //        return string.Empty;

        //    var snapshot = await fbData.fs
        //        .Collection("clubs")
        //        .GetAsync();

        //    foreach (var document in snapshot.Documents)
        //    {
        //        string? clubUserEmail = document.Data["userEmail"].ToString();

        //        if (clubUserEmail == userEmail)
        //        {
        //            return document.Data["name"].ToString();
        //        }
        //    }

        //    return string.Empty;
        //}

    }
}
