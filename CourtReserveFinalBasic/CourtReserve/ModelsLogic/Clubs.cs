using CourtReserve.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourtReserve.ModelsLogic
{
    class Clubs:ClubsModel
    {
        private readonly FbData fbData=new FbData();
        public override async Task LoadAsync()
        {
            string email = Preferences.Get(Keys.EmailKey, string.Empty);
            if (email != string.Empty)
            {
                await LoadByUserEmailAsync(email);
            }
        }
        public override void SelectClub(string club)
        {
            Preferences.Set(Keys.AdminSelectedClub, club);
        }
        public override void SelectClubClient(string club)
        {
            Preferences.Set(Keys.ClientSelectedClub, club);
        }
        protected override async Task LoadByUserEmailAsync(string email)
        {
            name = string.Empty;
            userEmail = string.Empty;

            IQuerySnapshot snapshot =
                await fbData.fs.Collection(ConstData.Clubs)
                    .WhereEqualsTo(Keys.UserEmail, email)
                    .GetAsync();

            List<AdminExistingClubsTextModel> clubItems = new List<AdminExistingClubsTextModel>();
            foreach (IDocumentSnapshot doc in snapshot.Documents)
            {
                ExtractClubData(doc, email);
                AdminExistingClubsTextModel item = CreateClubItemWithAvailability(doc, await CountAvailabilityForClub(doc));
                if (item != null)
                    clubItems.Add(item);
            }

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                clubs.Clear();
                foreach (AdminExistingClubsTextModel item in clubItems)
                    clubs.Add(item);
            });
        }
        public override async Task LoadAsyncClient()
        {
            name = string.Empty;
            userEmail = string.Empty;

            IQuerySnapshot snapshot =
                await fbData.fs.Collection(ConstData.Clubs)
                    .GetAsync();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                clubs.Clear();
                foreach (IDocumentSnapshot doc in snapshot.Documents)
                {
                    AddClubFromDocument(doc);
                }
            });
        }

        protected override async Task<IDocumentSnapshot?> FindClubByEmailAsync(string email)
        {
            IQuerySnapshot? snapshot =
                await fbData.fs.Collection(ConstData.Clubs).WhereEqualsTo(Keys.UserEmail, email).GetAsync();
            return snapshot.Documents.FirstOrDefault();
        }
        protected override void ExtractClubData(IDocumentSnapshot document, string email)
        {
            string? tempName = document.Get<string>(Keys.Name);
            string? tempEmail = document.Get<string>(Keys.UserEmail);
            if (tempName != null)
                name = tempName;
            if (tempEmail != null)
                userEmail = tempEmail;
            else
                userEmail = email;
        }
        protected override void AddClubFromDocument(IDocumentSnapshot document)
        {
            if (document.Data != null && document.Data.ContainsKey(Keys.ClubName))
            {
                string? club = document.Get<string>(Keys.ClubName);
                if (club != null && club != string.Empty && !clubs.Any(d => d.ClubText == club))
                    clubs.Add(new AdminExistingClubsText(club));
            }
        }

        public override async Task FilterClubsByAvailabilityAsync(string date, int hourIndex)
        {
            IQuerySnapshot clubsSnapshot = await fbData.fs.Collection(ConstData.Clubs).GetAsync();
            List<string> availableClubs = new List<string>();

            foreach (IDocumentSnapshot clubDoc in clubsSnapshot.Documents)
            {
                if (clubDoc.Data != null && clubDoc.Data.ContainsKey(Keys.ClubName))
                {
                    string? clubName = clubDoc.Get<string>(Keys.ClubName);
                    int courtsCount = 0;
                    if (clubDoc.Data.ContainsKey("courtsCount"))
                        courtsCount = clubDoc.Get<int>("courtsCount");

                    if (!string.IsNullOrWhiteSpace(clubName) && courtsCount > 0)
                    {
                        if (await HasFreeCourtAsync(clubName, date, hourIndex, courtsCount))
                            availableClubs.Add(clubName);
                    }
                }
            }

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                clubs.Clear();
                foreach (string club in availableClubs)
                    clubs.Add(new AdminExistingClubsText(club));
            });
        }

        protected AdminExistingClubsTextModel CreateClubItemWithAvailability(IDocumentSnapshot document, int[] availability)
        {
            string club = document.Get<string>(Keys.ClubName) ?? string.Empty;
            if (string.IsNullOrWhiteSpace(club))
                return null;

            return new AdminExistingClubsText(club, availability[0], availability[1]);
        }

        protected async Task<int[]> CountAvailabilityForClub(IDocumentSnapshot document)
        {
            int freeSlots = 0;
            int totalSlots = 0;

            if (document.Data != null && document.Data.ContainsKey(Keys.ClubName))
            {
                string? club = document.Get<string>(Keys.ClubName);
                if (!string.IsNullOrWhiteSpace(club))
                {
                    IQuerySnapshot courtDocs = await fbData.fs.Collection(club).GetAsync();

                    foreach (IDocumentSnapshot courtDoc in courtDocs.Documents)
                    {
                        if (courtDoc.Data != null && courtDoc.Data.ContainsKey(Keys.LclientsField))
                        {
                            IList<Client>? clientsList = courtDoc.Get<IList<Client>>(Keys.LclientsField);
                            if (clientsList != null)
                            {
                                totalSlots += clientsList.Count;
                                foreach (Client c in clientsList)
                                {
                                    if (c.UserId == string.Empty && c.Name == string.Empty)
                                        freeSlots++;
                                }
                            }
                        }
                    }
                }
            }

            return new int[] { freeSlots, totalSlots };
        }

        protected async Task<bool> HasFreeCourtAsync(string clubName, string date, int hourIndex, int courtsCount)
        {
            for (int court = 1; court <= courtsCount; court++)
            {
                string docId = $"{court}_{date}";
                IDocumentReference docRef = fbData.fs.Collection(clubName).Document(docId);
                IDocumentSnapshot snapshot = await docRef.GetAsync();

                if (snapshot.Exists && snapshot.Data != null)
                {
                    IList<Client>? clientsList = snapshot.Get<IList<Client>>(Keys.LclientsField);
                    if (clientsList != null && hourIndex >= 0 && hourIndex < clientsList.Count)
                    {
                        if (clientsList[hourIndex].UserId == string.Empty && clientsList[hourIndex].Name == string.Empty)
                            return true;
                    }
                }
            }
            return false;
        }

        public override List<string> GetDateOptions()
        {
            List<string> dates = new List<string>();
            DateTime today = DateTime.Today;
            for (int i = 0; i < ConstData.DaysInWeek; i++)
                dates.Add(today.AddDays(i).ToString(ConstData.DateFormat));
            return dates;
        }

        public override List<string> GetHourOptions()
        {
            List<string> hours = new List<string>();
            for (int i = 0; i < ConstData.HoursPerDay; i++)
            {
                int hour = i + 6;
                hours.Add(hour.ToString("00") + ":00");
            }
            return hours;
        }

        public override int HourTextToIndex(string hourText)
        {
            List<string> hours = GetHourOptions();
            return hours.IndexOf(hourText);
        }

        public override async Task EnsureAllClubsHaveDocumentsAsync()
        {
            IQuerySnapshot snapshot = await fbData.fs.Collection(ConstData.Clubs).GetAsync();
            Club club = new Club();

            foreach (IDocumentSnapshot doc in snapshot.Documents)
            {
                if (doc.Data != null && doc.Data.ContainsKey(Keys.ClubName))
                {
                    string? clubName = doc.Get<string>(Keys.ClubName);
                    if (!string.IsNullOrWhiteSpace(clubName))
                        await club.EnsureDocumentsForNextWeekAsync(clubName);
                }
            }
        }
    }
}
