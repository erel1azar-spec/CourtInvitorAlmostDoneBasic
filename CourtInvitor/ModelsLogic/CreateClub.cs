using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
{
    internal class CreateClub:CreateClubModel
    {
        private readonly FbData fbData = new();

        public override string ClubName { get; set; } = string.Empty;
        public override string Location { get; set; } = string.Empty;
        public override string Phone { get; set; } = string.Empty;
        public override string Email { get; set; } = string.Empty;
        public override int CourtsCount { get; set; } = 1;
        public override string StatusMessage { get; set; } = string.Empty;

        public override async Task<bool> CreateClubAsync(
            DateTime startDate,
            Action<Task> onComplete)
        {
            if (string.IsNullOrWhiteSpace(ClubName))
            {
                StatusMessage = "Club name cannot be empty.";
                return false;
            }

            // בדיקה אם מועדון כבר קיים
            var clubsColl = fbData.fs.Collection("clubs");
            var existing = await clubsColl
                .WhereEqualsTo("name", ClubName)
                .GetAsync();

            if (existing.Count > 0)
            {
                StatusMessage = "Club already exists!";
                return false;
            }

            string loggedInEmail =
                Preferences.Get(Keys.EmailKey, string.Empty);

            // יצירת מסמך מועדון
            var clubDoc = new
            {
                name = ClubName,
                location = Location,
                phone = Phone,
                email = Email,
                userEmail = loggedInEmail,
                courtsCount = CourtsCount
            };

            fbData.SetDocument(clubDoc, "clubs", string.Empty, onComplete);

            // יצירת collection בשם המועדון
            var clubCollection = fbData.fs.Collection(ClubName);

            for (int court = 1; court <= CourtsCount; court++)
            {
                for (int day = 0; day < 7; day++)
                {
                    DateTime date = startDate.AddDays(day);
                    string dateKey = date.ToString("dd.MM.yyyy");

                    var clients = new List<Client>();
                    for (int i = 0; i < 17; i++)
                    {
                        clients.Add(new Client()); // לקוח ריק = שעה פנויה
                    }

                    var courtDoc = new
                    {
                        date = dateKey,
                        courtNumber = court,
                        clients = clients
                    };

                    fbData.SetDocument(
                        courtDoc,
                        ClubName,
                        $"{court}_{dateKey}",
                        onComplete);
                }
            }

            StatusMessage = "Club created successfully!";
            return true;
        }
        //private readonly FbData fbData = new();

        //public override string ClubName { get; set; } = string.Empty;
        //public override string Location { get; set; } = string.Empty;
        //public override string Phone { get; set; } = string.Empty;
        //public override string Email { get; set; } = string.Empty;
        //public override int CourtsCount { get; set; } = 1;
        //public override string StatusMessage { get; set; } = string.Empty;

        //    public override async Task<bool> CreateClubAsync(DateTime startDate, Action<System.Threading.Tasks.Task> onComplete)
        //{
        //    if (string.IsNullOrWhiteSpace(ClubName))
        //    {
        //        StatusMessage = "Club name cannot be empty.";
        //        return false;
        //    }

        //    var clubsColl = fbData.fs.Collection("clubs");
        //    var querySnapshot = await clubsColl.WhereEqualsTo("name", ClubName).GetAsync();

        //    if (querySnapshot.Count > 0)
        //    {
        //        StatusMessage = "Club already exists!";
        //        return false;
        //    }
        //    string loggedInEmail = Preferences.Get(Keys.EmailKey, string.Empty);

        //    var clubDocObj = new
        //    {
        //        name = ClubName,
        //        location = Location,
        //        phone = Phone,
        //        email = Email,
        //        userEmail = loggedInEmail,
        //        courtsCount = CourtsCount,
        //    };
        //    fbData.SetDocument(clubDocObj, "clubs", String.Empty, onComplete);

        //    // 3️⃣ יוצרים collection בשם המועדון עצמו
        //    var clubCollection = fbData.fs.Collection(ClubName);

        //    for (int court = 1; court <= CourtsCount; court++)
        //    {
        //        for (int day = 0; day < 7; day++)
        //        {
        //            DateTime date = startDate.AddDays(day);
        //            string dateKey = date.ToString("dd.MM.yyyy");
        //            string[] clients = new string[17];
        //            for (int i = 0; i < 17; i++)
        //            {
        //                clients[i] = string.Empty;
        //            }

        //            var docObj = new
        //            {
        //                date = dateKey,
        //                courtNumber = court,
        //                clients= clients
        //            };

        //            fbData.SetDocument(docObj, ClubName, String.Empty, onComplete);
        //        }
        //    }

        //    StatusMessage = "Club created successfully!";
        //    return true;
        //}
    }
}
