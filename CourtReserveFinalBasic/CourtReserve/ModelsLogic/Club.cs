using CourtReserve.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class Club:ClubModel
    {
        private readonly FbData data= new FbData();
        public override string ClubName { get; set; } = string.Empty;
        public override string Location { get; set; } = string.Empty;
        public override string Phone { get; set; } = string.Empty;
        public override string Email { get; set; } = string.Empty;
        public override int CourtsCount { get; set; } = 1;
        public Club()
        {
        }
        public override async Task CreateClubAsync(DateTime startDate)
        {
            isSuccess = false;
            string validationError = ValidateFields();
            if (validationError != string.Empty)
            {
                statusMessage = validationError;
            }
            else if (await ClubExistsAsync())
            {
                statusMessage = Strings.ClubAlreadyExists;
            }
            else
            {
                SaveClubToFirestore();
                CreateCourtsForWeek(startDate);
                statusMessage = Strings.ClubCreatedSuccess;
                isSuccess = true;
            }
        }
        protected override void SaveClubToFirestore()
        {
            string loggedInEmail = Preferences.Get(Keys.EmailKey, string.Empty);
            object clubDoc = new
            {
                name = ClubName,
                location = Location,
                phone = Phone,
                email = Email,
                userEmail = loggedInEmail,
                courtsCount = CourtsCount
            };
            data.SetDocument(clubDoc, ConstData.Clubs, string.Empty, t => { });
        }
        protected override void CreateCourtsForWeek(DateTime startDate)
        {
            for (int court = 1; court <= CourtsCount; court++)
                for (int day = 0; day < ConstData.DaysInWeek; day++)
                    CreateCourtDay(court, startDate.AddDays(day));
        }
        protected override void CreateCourtDay(int courtNumber, DateTime date)
        {
            string dateKey = date.ToString(ConstData.DateFormat);
            List<Client> clientsList = new List<Client>();
            for (int i = 0; i < ConstData.HoursPerDay; i++)
                clientsList.Add(new Client());
            object courtDoc = new
            {
                date = dateKey,
                CourtNumber = courtNumber,
                Lclients = clientsList
            };
            data.SetDocument(courtDoc, ClubName, $"{courtNumber}_{dateKey}", t => { });
        }
        protected override string ValidateFields()
        {
            string error = string.Empty;
            string loggedInEmail = Preferences.Get(Keys.EmailKey, string.Empty);
            if (string.IsNullOrWhiteSpace(loggedInEmail))
            {
                error = Strings.NotLoggedIn;
            }
            else if (string.IsNullOrWhiteSpace(ClubName))
            {
                error = Strings.ClubNameEmpty;
            }
            else if (string.IsNullOrWhiteSpace(Location))
            {
                error = Strings.LocationEmpty;
            }
            else if (string.IsNullOrWhiteSpace(Phone))
            {
                error = Strings.PhoneEmpty;
            }
            else if (!IsPhoneValid(Phone))
            {
                error = Strings.PhoneInvalid;
            }
            else if (string.IsNullOrWhiteSpace(Email))
            {
                error = Strings.ClubEmailEmpty;
            }
            else if (!IsEmailValid(Email))
            {
                error = Strings.ClubEmailInvalid;
            }
            return error;
        }
        protected override async Task<bool> ClubExistsAsync()
        {
            bool clubExists = false;
            TaskCompletionSource<bool> taskCompletion = new TaskCompletionSource<bool>();
            data.GetDocumentsWhereEqualTo(ConstData.Clubs, Keys.Name, ClubName,
                qs =>
                {
                    foreach (IDocumentSnapshot doc in qs.Documents)
                        clubExists = true;
                    taskCompletion.SetResult(true);
                });
            await taskCompletion.Task;
            return clubExists;
        }
        protected override bool IsPhoneValid(string phone)
        {
            bool isValid = true;
            for (int i = 0; i < phone.Length && isValid; i++)
            {
                char c = phone[i];
                if (c < '0' || c > '9')
                    isValid = false;
            }
            return isValid;
        }

        protected override bool IsEmailValid(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }

        public async Task<int> GetClubCourtsCountAsync(string clubName)
        {
            int courtsCount = 0;
            IQuerySnapshot snapshot = await data.fs.Collection(ConstData.Clubs)
                .WhereEqualsTo(Keys.Name, clubName)
                .GetAsync();

            IDocumentSnapshot? doc = snapshot.Documents.FirstOrDefault();
            if (doc != null && doc.Data != null && doc.Data.ContainsKey("courtsCount"))
            {
                courtsCount = doc.Get<int>("courtsCount");
            }
            return courtsCount;
        }

        public async Task EnsureDocumentsForNextWeekAsync(string clubName)
        {
            if (!string.IsNullOrWhiteSpace(clubName))
            {
                int courtsCount = await GetClubCourtsCountAsync(clubName);
                if (courtsCount > 0)
                {
                    DateTime today = DateTime.Today;
                    for (int day = 0; day < ConstData.DaysInWeek; day++)
                    {
                        DateTime targetDate = today.AddDays(day);
                        await EnsureDocumentsForDateAsync(clubName, targetDate, courtsCount);
                    }
                }
            }
        }

        protected async Task EnsureDocumentsForDateAsync(string clubName, DateTime date, int courtsCount)
        {
            string dateKey = date.ToString(ConstData.DateFormat);

            for (int court = 1; court <= courtsCount; court++)
            {
                string docId = $"{court}_{dateKey}";
                IDocumentReference docRef = data.fs.Collection(clubName).Document(docId);
                IDocumentSnapshot snapshot = await docRef.GetAsync();

                if (!snapshot.Exists)
                {
                    CreateCourtDayForClub(clubName, court, date);
                }
            }
        }

        protected void CreateCourtDayForClub(string clubName, int courtNumber, DateTime date)
        {
            string dateKey = date.ToString(ConstData.DateFormat);
            List<Client> clientsList = new List<Client>();
            for (int i = 0; i < ConstData.HoursPerDay; i++)
                clientsList.Add(new Client());
            object courtDoc = new
            {
                date = dateKey,
                CourtNumber = courtNumber,
                Lclients = clientsList
            };
            data.SetDocument(courtDoc, clubName, $"{courtNumber}_{dateKey}", t => { });
        }
    }
}
