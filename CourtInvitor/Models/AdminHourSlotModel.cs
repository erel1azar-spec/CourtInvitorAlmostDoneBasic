namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model representing an hour slot for admin booking view.
    /// </summary>
    public abstract class AdminHourSlotModel
    {
        /// <summary>
        /// Gets the hour index (0-16).
        /// </summary>
        public abstract int Index { get; }
        /// <summary>
        /// Gets the client name for this slot.
        /// </summary>
        public abstract string ClientName { get; }
        /// <summary>
        /// Gets the time text for this slot.
        /// </summary>
        public abstract string TimeText { get; }
        /// <summary>
        /// Gets the status text showing client or available.
        /// </summary>
        public abstract string StatusText { get; }
        /// <summary>
        /// Gets whether this slot is booked.
        /// </summary>
        public abstract bool IsBooked { get; }
        /// <summary>
        /// Gets the text color for the status.
        /// </summary>
        public abstract Color StatusTextColor { get; }
    }
}
