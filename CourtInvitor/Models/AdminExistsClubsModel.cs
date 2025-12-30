namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for admin existing clubs.
    /// </summary>
    public abstract class AdminExistsClubsModel
    {
        /// <summary>
        /// Gets the club name.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Gets the user email associated with the club.
        /// </summary>
        public abstract string UserEmail { get; }
    }
}
