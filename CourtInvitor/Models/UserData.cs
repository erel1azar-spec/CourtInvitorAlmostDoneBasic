namespace CourtInvitor.Models
{
    /// <summary>
    /// Data transfer object for user information from Firebase.
    /// </summary>
    internal class UserData
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string userName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string email { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        public string role { get; set; } = string.Empty;
    }
}
