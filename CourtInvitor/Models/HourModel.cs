namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for hour slot.
    /// </summary>
    public abstract class HourModel : ObservableObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the hour index.
        /// </summary>
        public abstract int Index { get; set; }
        /// <summary>
        /// Gets the time text.
        /// </summary>
        public abstract string TimeText { get; }
        /// <summary>
        /// Gets or sets whether hour is available.
        /// </summary>
        public abstract bool IsAvailable { get; set; }
        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public abstract string ClientName { get; set; }
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        public abstract string ClientId { get; set; }
        #endregion
        #region Protected Functions
        /// <summary>
        /// Formats hour index to time text.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>Formatted time string.</returns>
        protected abstract string FormatHour(int hourIndex);
        #endregion
    }
}

