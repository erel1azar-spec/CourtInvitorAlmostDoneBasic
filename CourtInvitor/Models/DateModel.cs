namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for court date schedule.
    /// </summary>
    public abstract class DateModel : ObservableObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the date string.
        /// </summary>
        public abstract string Date { get; set; }
        /// <summary>
        /// Gets or sets the court number.
        /// </summary>
        public abstract int CourtNumber { get; set; }
        /// <summary>
        /// Gets the collection of hours for this date.
        /// </summary>
        public abstract System.Collections.ObjectModel.ObservableCollection<HourModel> Hours { get; }
        /// <summary>
        /// Gets the display text for date.
        /// </summary>
        public abstract string DisplayText { get; }
        #endregion
        #region Public Functions
        /// <summary>
        /// Checks if specific hour is available.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>True if hour is available.</returns>
        public abstract bool IsHourAvailable(int hourIndex);
        /// <summary>
        /// Gets client name for specific hour.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>Client name or empty string.</returns>
        public abstract string GetClientAtHour(int hourIndex);
        #endregion
    }
}

