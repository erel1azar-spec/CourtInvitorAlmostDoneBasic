namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for client existing courts list.
    /// </summary>
    public abstract class ClientExistingCourtsListModel
    {
        /// <summary>
        /// Gets the court number.
        /// </summary>
        public abstract int CourtNumber { get; }
        /// <summary>
        /// Gets the court display text.
        /// </summary>
        public abstract string CourtText { get; }
    }
}
