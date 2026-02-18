using System.Collections.ObjectModel;
namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for admin user functionality.
    /// </summary>
    public abstract class AdminModel : UserModel
    {
        #region Properties
        /// <summary>
        /// Gets the collection of managed clubs.
        /// </summary>
        public abstract ObservableCollection<ClubModel> ManagedClubs { get; }
        #endregion
        #region Public Functions
        /// <summary>
        /// Loads clubs managed by this admin.
        /// </summary>
        public abstract Task LoadManagedClubsAsync();
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
        public abstract Task<bool> CreateClubAsync(string name, string location, string phone, string email, int courtsCount, DateTime startDate);
        /// <summary>
        /// Gets all clients who made reservations at a club on specific date.
        /// </summary>
        /// <param name="clubName">The club name.</param>
        /// <param name="date">The date.</param>
        /// <returns>Collection of hour models with client information.</returns>
        public abstract Task<ObservableCollection<HourModel>> GetClubClientsAsync(string clubName, string date);
        #endregion
    }
}

