namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for club creation.
    /// </summary>
    internal abstract class CreateClubModel
    {
        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public abstract string ClubName { get; set; }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public abstract string Location { get; set; }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public abstract string Phone { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public abstract string Email { get; set; }
        /// <summary>
        /// Gets or sets the courts count.
        /// </summary>
        public abstract int CourtsCount { get; set; }
        /// <summary>
        /// Gets the status message.
        /// </summary>
        public abstract string StatusMessage { get; }
        /// <summary>
        /// Gets whether the last operation was successful.
        /// </summary>
        public abstract bool IsSuccess { get; }
        /// <summary>
        /// Creates a new club asynchronously.
        /// </summary>
        /// <param name="startDate">The start date for court scheduling.</param>
        public abstract Task CreateClubAsync(DateTime startDate);
    }
}

