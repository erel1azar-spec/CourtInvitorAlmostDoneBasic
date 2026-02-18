using System.Collections.ObjectModel;
namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for client user functionality.
    /// </summary>
    public abstract class ClientModel : UserModel
    {
        #region Properties
        /// <summary>
        /// Gets the collection of available clubs.
        /// </summary>
        public abstract ObservableCollection<ClubModel> AvailableClubs { get; }
        /// <summary>
        /// Gets the collection of user reservations.
        /// </summary>
        public abstract ObservableCollection<HourModel> MyReservations { get; }
        #endregion
        #region Public Functions
        /// <summary>
        /// Loads available clubs from database.
        /// </summary>
        public abstract Task LoadAvailableClubsAsync();
        /// <summary>
        /// Loads user's reservations from database.
        /// </summary>
        public abstract Task LoadMyReservationsAsync();
        /// <summary>
        /// Makes a reservation for specific court and hour.
        /// </summary>
        /// <param name="clubName">The club name.</param>
        /// <param name="courtNumber">The court number.</param>
        /// <param name="date">The date.</param>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>True if reservation succeeded.</returns>
        public abstract Task<bool> MakeReservationAsync(string clubName, int courtNumber, string date, int hourIndex);
        #endregion
    }
}

