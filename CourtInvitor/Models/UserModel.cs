namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract base class for user authentication and management.
    /// </summary>
    public abstract class UserModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        public string Role { get; set; } = string.Empty;
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>True if registration succeeded.</returns>
        public abstract Task<bool> Register();
        /// <summary>
        /// Signs out the current user.
        /// </summary>
        public abstract void SignOut();
        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <returns>True if login succeeded.</returns>
        public abstract Task<bool> Login();
        /// <summary>
        /// Checks if registration is possible with current credentials.
        /// </summary>
        /// <returns>True if registration can proceed.</returns>
        public abstract bool CanRegister();
    }
}
