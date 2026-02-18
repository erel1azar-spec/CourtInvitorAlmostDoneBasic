using System.Collections.ObjectModel;
namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for sports club.
    /// </summary>
    public abstract class ClubModel : ObservableObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public abstract string Name { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public abstract string Location { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public abstract string Phone { get; set; }
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public abstract string Email { get; set; }
        /// <summary>
        /// Gets or sets the number of courts.
        /// </summary>
        public abstract int CourtsCount { get; set; }
        /// <summary>
        /// Gets the collection of courts.
        /// </summary>
        public abstract ObservableCollection<CourtModel> Courts { get; }
        #endregion
        #region Public Functions
        /// <summary>
        /// Initializes courts for the club.
        /// </summary>
        /// <param name="startDate">Start date for scheduling.</param>
        public abstract Task InitializeCourtsAsync(DateTime startDate);
        /// <summary>
        /// Gets courts schedule for specific date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Collection of dates with court schedules.</returns>
        public abstract Task<ObservableCollection<DateModel>> GetCourtsForDateAsync(string date);
        #endregion
    }
}

