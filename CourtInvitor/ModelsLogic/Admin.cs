using CourtInvitor.Models;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of admin user functionality.
    /// </summary>
    public class Admin : AdminModel
    {
        #region Fields
        private readonly FbData fbData;
        private ObservableCollection<ClubModel> managedClubs;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of managed clubs.
        /// </summary>
        public override ObservableCollection<ClubModel> ManagedClubs => managedClubs;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Admin class.
        /// </summary>
        public Admin()
        {
            fbData = new FbData();
            managedClubs = new ObservableCollection<ClubModel>();
            Email = Preferences.Get(Keys.EmailKey, string.Empty);
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Loads clubs managed by this admin.
        /// </summary>
        public override async Task LoadManagedClubsAsync()
        {
            managedClubs.Clear();
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            fbData.GetDocumentsWhereEqualTo(ConstData.Clubs, Keys.UserEmail, Email, qs =>
            {
                foreach (IDocumentSnapshot doc in qs.Documents)
                    AddClubFromDocument(doc);
                tcs.SetResult(true);
            });
            await tcs.Task;
        }
        /// <summary>
        /// Creates a new club.
        /// </summary>
        /// <param name="name">Club name.</param>
        /// <param name="location">Club location.</param>
        /// <param name="phone">Phone number.</param>
        /// <param name="email">Email address.</param>
        /// <param name="courtsCount">Number of courts.</param>
        /// <param name="startDate">Start date for scheduling.</param>
        /// <returns>True if club created successfully.</returns>
        public override async Task<bool> CreateClubAsync(string name, string location, string phone, string email, int courtsCount, DateTime startDate)
        {
            if (!ValidateClubData(name, location, phone, email))
                return false;
            if (await ClubExistsAsync(name))
            {
                await Shell.Current.DisplayAlert(Strings.Error, Strings.ClubAlreadyExists, Strings.Ok);
                return false;
            }
            SaveClubToFirestore(name, location, phone, email, courtsCount);
            await InitializeClubCourts(name, courtsCount, startDate);
            return true;
        }
        /// <summary>
        /// Gets all clients who made reservations at a club on specific date.
        /// </summary>
        /// <param name="clubName">The club name.</param>
        /// <param name="date">The date.</param>
        /// <returns>Collection of hour models with client information.</returns>
        public override async Task<ObservableCollection<HourModel>> GetClubClientsAsync(string clubName, string date)
        {
            ObservableCollection<HourModel> clients = new ObservableCollection<HourModel>();
            int courtsCount = await GetClubCourtsCountAsync(clubName);
            for (int courtNum = 1; courtNum <= courtsCount; courtNum++)
            {
                string docId = $"{courtNum}_{date}";
                IDocumentSnapshot doc = await fbData.fs.Collection(clubName).Document(docId).GetAsync();
                if (doc.Exists)
                    ExtractClientsFromDocument(doc, courtNum, clients);
            }
            return clients;
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Validates club data fields.
        /// </summary>
        /// <param name="name">Club name.</param>
        /// <param name="location">Location.</param>
        /// <param name="phone">Phone number.</param>
        /// <param name="email">Email address.</param>
        /// <returns>True if all fields are valid.</returns>
        private static bool ValidateClubData(string name, string location, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location))
                return false;
            if (string.IsNullOrWhiteSpace(phone) || !IsPhoneValid(phone))
                return false;
            if (string.IsNullOrWhiteSpace(email) || !IsEmailValid(email))
                return false;
            return true;
        }
        /// <summary>
        /// Checks if phone number is valid.
        /// </summary>
        /// <param name="phone">Phone number.</param>
        /// <returns>True if valid.</returns>
        private static bool IsPhoneValid(string phone)
        {
            foreach (char c in phone)
                if (c < '0' || c > '9')
                    return false;
            return true;
        }
        /// <summary>
        /// Checks if email format is valid.
        /// </summary>
        /// <param name="email">Email address.</param>
        /// <returns>True if valid.</returns>
        private static bool IsEmailValid(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }
        /// <summary>
        /// Checks if club with given name already exists.
        /// </summary>
        /// <param name="clubName">Club name.</param>
        /// <returns>True if club exists.</returns>
        private async Task<bool> ClubExistsAsync(string clubName)
        {
            bool exists = false;
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            fbData.GetDocumentsWhereEqualTo(ConstData.Clubs, Keys.Name, clubName, qs =>
            {
                exists = qs.Documents.Count > 0;
                tcs.SetResult(true);
            });
            await tcs.Task;
            return exists;
        }
        /// <summary>
        /// Saves club to Firestore.
        /// </summary>
        /// <param name="name">Club name.</param>
        /// <param name="location">Location.</param>
        /// <param name="phone">Phone.</param>
        /// <param name="email">Email.</param>
        /// <param name="courtsCount">Courts count.</param>
        private void SaveClubToFirestore(string name, string location, string phone, string email, int courtsCount)
        {
            object clubDoc = new { name = name, location = location, phone = phone, email = email, userEmail = Email, courtsCount = courtsCount };
            fbData.SetDocument(clubDoc, ConstData.Clubs, string.Empty, _ => { });
        }
        /// <summary>
        /// Initializes courts for club.
        /// </summary>
        /// <param name="clubName">Club name.</param>
        /// <param name="courtsCount">Number of courts.</param>
        /// <param name="startDate">Start date.</param>
        private async Task InitializeClubCourts(string clubName, int courtsCount, DateTime startDate)
        {
            for (int courtNum = 1; courtNum <= courtsCount; courtNum++)
                for (int day = 0; day < ConstData.DaysInWeek; day++)
                    CreateCourtDay(clubName, courtNum, startDate.AddDays(day));
            await Task.CompletedTask;
        }
        /// <summary>
        /// Creates single court day document.
        /// </summary>
        /// <param name="clubName">Club name.</param>
        /// <param name="courtNumber">Court number.</param>
        /// <param name="date">Date.</param>
        private void CreateCourtDay(string clubName, int courtNumber, DateTime date)
        {
            string dateKey = date.ToString(ConstData.DateFormat);
            System.Collections.Generic.List<object> clients = new System.Collections.Generic.List<object>();
            for (int i = 0; i < ConstData.HoursPerDay; i++)
                clients.Add(new { Name = string.Empty, UserId = string.Empty });
            object courtDoc = new { date = dateKey, CourtNumber = courtNumber, Lclients = clients };
            fbData.SetDocument(courtDoc, clubName, $"{courtNumber}_{dateKey}", _ => { });
        }
        /// <summary>
        /// Adds club from Firestore document.
        /// </summary>
        /// <param name="document">Firestore document.</param>
        private void AddClubFromDocument(IDocumentSnapshot document)
        {
            System.Collections.Generic.IDictionary<string, object>? data = document.Data;
            if (data != null)
            {
                Club club = new Club
                {
                    Name = data.ContainsKey(Keys.Name) ? data[Keys.Name]?.ToString() ?? string.Empty : string.Empty,
                    Location = data.ContainsKey(Keys.Location) ? data[Keys.Location]?.ToString() ?? string.Empty : string.Empty,
                    Phone = data.ContainsKey(Keys.Phone) ? data[Keys.Phone]?.ToString() ?? string.Empty : string.Empty,
                    Email = data.ContainsKey(Keys.Email) ? data[Keys.Email]?.ToString() ?? string.Empty : string.Empty,
                    CourtsCount = data.ContainsKey(Keys.CourtsCount) ? Convert.ToInt32(data[Keys.CourtsCount]) : 0
                };
                managedClubs.Add(club);
            }
        }
        /// <summary>
        /// Gets club courts count.
        /// </summary>
        /// <param name="clubName">Club name.</param>
        /// <returns>Courts count.</returns>
        private async Task<int> GetClubCourtsCountAsync(string clubName)
        {
            int courtsCount = 0;
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            fbData.GetDocumentsWhereEqualTo(ConstData.Clubs, Keys.Name, clubName, qs =>
            {
                if (qs.Documents.Count > 0)
                {
                    System.Collections.Generic.IDictionary<string, object>? data = qs.Documents[0].Data;
                    courtsCount = data?.ContainsKey(Keys.CourtsCount) == true ? Convert.ToInt32(data[Keys.CourtsCount]) : 0;
                }
                tcs.SetResult(true);
            });
            await tcs.Task;
            return courtsCount;
        }
        /// <summary>
        /// Extracts clients from court document.
        /// </summary>
        /// <param name="document">Firestore document.</param>
        /// <param name="courtNumber">Court number.</param>
        /// <param name="clients">Collection to add clients to.</param>
        private static void ExtractClientsFromDocument(IDocumentSnapshot document, int courtNumber, ObservableCollection<HourModel> clients)
        {
            System.Collections.Generic.IDictionary<string, object>? data = document.Data;
            if (data != null && data.ContainsKey("Lclients"))
            {
                System.Collections.IList? clientsList = data["Lclients"] as System.Collections.IList;
                if (clientsList != null)
                {
                    for (int i = 0; i < clientsList.Count; i++)
                    {
                        System.Collections.Generic.IDictionary<string, object>? clientData = clientsList[i] as System.Collections.Generic.IDictionary<string, object>;
                        if (clientData != null)
                        {
                            string clientName = clientData.ContainsKey("Name") ? clientData["Name"]?.ToString() ?? string.Empty : string.Empty;
                            string userId = clientData.ContainsKey("UserId") ? clientData["UserId"]?.ToString() ?? string.Empty : string.Empty;
                            if (!string.IsNullOrEmpty(userId))
                            {
                                Hour hour = new Hour(i) { ClientName = clientName, ClientId = userId };
                                clients.Add(hour);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}

