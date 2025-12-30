namespace CourtInvitor.Models
{
    /// <summary>
    /// Represents a court schedule for a specific day.
    /// </summary>
    internal class CourtDay
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the court number.
        /// </summary>
        public int CourtNumber { get; set; }
        /// <summary>
        /// Gets or sets the list of clients.
        /// </summary>
        public List<Client> Clients { get; set; } = new();
    }
}
