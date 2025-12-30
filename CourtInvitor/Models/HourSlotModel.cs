namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model representing an hour slot.
    /// </summary>
    public abstract class HourSlotModel
    {
        /// <summary>
        /// Gets the hour index.
        /// </summary>
        public abstract int Index { get; }
        /// <summary>
        /// Gets the time text.
        /// </summary>
        public abstract string TimeText { get; }
    }
}
