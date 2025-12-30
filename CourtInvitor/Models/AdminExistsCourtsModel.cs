namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for admin existing courts.
    /// </summary>
    public abstract class AdminExistsCourtsModel
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
