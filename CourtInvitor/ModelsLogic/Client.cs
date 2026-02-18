using CourtInvitor.Models;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of client user functionality.
    /// </summary>
    public class Client : ClientModel
    {
        #region Fields
        private readonly FbData fbData;
        private ObservableCollection<ClubModel> availableClubs;
        private ObservableCollection<HourModel> myReservations;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection of available clubs.
        /// </summary>
        public override ObservableCollection<ClubModel> AvailableClubs => availableClubs;
        /// <summary>
        /// Gets the collection of user reservations.
        /// </summary>
        public override ObservableCollection<HourModel> MyReservations => myReservations;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Client class.
        /// </summary>
        public Client()
        {
            fbData = new FbData();
            availableClubs = new ObservableCollection<ClubModel>();
            myReservations = new ObservableCollection<HourModel>();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Loads available clubs from database.
        /// </summary>
        public override async Task LoadAvailableClubsAsync()
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            fbData.AddSnapshotListener(ConstData.Clubs, qs =>
            {
                availableClubs.Clear();
                if (qs != null)
                    foreach (IDocumentSnapshot doc in qs.Documents)
                        AddClubFromDocument(doc);
                tcs.TrySetResult(true);
            });
            await tcs.Task;
        }
        /// <summary>
        /// Loads user's reservations from database.
        /// </summary>
        public override async Task LoadMyReservationsAsync()
        {
            myReservations.Clear();
            await Task.CompletedTask;
        }
        /// <summary>
        /// Makes a reservation for specific court and hour.
        /// </summary>
        /// <param name="clubName">The club name.</param>
        /// <param name="courtNumber">The court number.</param>
        /// <param name="date">The date.</param>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>True if reservation succeeded.</returns>
        public override async Task<bool> MakeReservationAsync(string clubName, int courtNumber, string date, int hourIndex)
        {
            string docId = $"{courtNumber}_{date}";
            IDocumentSnapshot doc = await fbData.fs.Collection(clubName).Document(docId).GetAsync();
            if (!doc.Exists)
                return false;
            string userId = Preferences.Get(Keys.UserIdKey, string.Empty);
            string userName = Preferences.Get(Keys.UserNameKey, string.Empty);
            System.Collections.Generic.Dictionary<string, object> updates = new System.Collections.Generic.Dictionary<string, object>
            {
                { $"Lclients.{hourIndex}.Name", userName },
                { $"Lclients.{hourIndex}.UserId", userId }
            };
            try
            {
                await fbData.fs.Collection(clubName).Document(docId).UpdateAsync(updates);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Adds club from Firestore document.
        /// </summary>
        /// <param name="document">The Firestore document.</param>
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
                availableClubs.Add(club);
            }
        }
        #endregion
    }
}

