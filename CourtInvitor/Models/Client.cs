namespace CourtInvitor.Models
{
    /// <summary>
    /// Represents a client booking a court.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the client user ID.
        /// </summary>
        public string UserId { get; set; } = string.Empty;
    }
}
