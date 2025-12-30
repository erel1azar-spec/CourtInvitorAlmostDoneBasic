using CourtInvitor.Models;
using Plugin.CloudFirestore;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of club creation logic.
    /// </summary>
    internal class CreateClub : CreateClubModel
    {
        #region Fields
        private readonly FbData data = new FbData();
        private string statusMessage = string.Empty;
        private bool isSuccess = false;
        #endregion
        #region Properties
        public override string ClubName { get; set; } = string.Empty;
        public override string Location { get; set; } = string.Empty;
        public override string Phone { get; set; } = string.Empty;
        public override string Email { get; set; } = string.Empty;
        public override int CourtsCount { get; set; } = 1;
        public override string StatusMessage => statusMessage;
        public override bool IsSuccess => isSuccess;
        #endregion
        #region Public Functions
        /// <summary>
        /// Creates a new club with courts for the week.
        /// </summary>
        /// <param name="startDate">The start date for court scheduling.</param>
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
        #endregion
        #region Private Functions
        /// <summary>
        /// Checks if a club with the same name already exists.
        /// </summary>
        /// <returns>True if club exists.</returns>
        private async Task<bool> ClubExistsAsync()
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
        /// <summary>
        /// Saves the club document to Firestore.
        /// </summary>
        private void SaveClubToFirestore()
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
        /// <summary>
        /// Creates court days for the entire week.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        private void CreateCourtsForWeek(DateTime startDate)
        {
            for (int court = 1; court <= CourtsCount; court++)
                for (int day = 0; day < ConstData.DaysInWeek; day++)
                    CreateCourtDay(court, startDate.AddDays(day));
        }
        /// <summary>
        /// Creates a single court day document.
        /// </summary>
        /// <param name="courtNumber">The court number.</param>
        /// <param name="date">The date for this court.</param>
        private void CreateCourtDay(int courtNumber, DateTime date)
        {
            string dateKey = date.ToString(ConstData.DateFormat);
            List<Client> clients = new List<Client>();
            for (int i = 0; i < ConstData.HoursPerDay; i++)
                clients.Add(new Client());
            object courtDoc = new
            {
                date = dateKey,
                CourtNumber = courtNumber,
                Lclients = clients
            };
            data.SetDocument(courtDoc, ClubName, $"{courtNumber}_{dateKey}", t => { });
        }
        /// <summary>
        /// Validates all input fields.
        /// </summary>
        /// <returns>Error message if validation fails, empty string if valid.</returns>
        private string ValidateFields()
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
        /// <summary>
        /// Checks if phone number contains only digits.
        /// </summary>
        /// <param name="phone">The phone number to validate.</param>
        /// <returns>True if valid.</returns>
        private static bool IsPhoneValid(string phone)
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
        /// <summary>
        /// Checks if email format is valid.
        /// </summary>
        /// <param name="email">The email to validate.</param>
        /// <returns>True if valid.</returns>
        private static bool IsEmailValid(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }
        #endregion
    }
}
