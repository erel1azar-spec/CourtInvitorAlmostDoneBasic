namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for session management.
    /// </summary>
    public abstract class SessionModel
    {
        /// <summary>
        /// Gets or sets the remaining time display text.
        /// </summary>
        public abstract string TimeLeft { get; protected set; }
        /// <summary>
        /// Registers the timer for session tracking.
        /// </summary>
        public abstract void RegisterTimer();
    }
}
