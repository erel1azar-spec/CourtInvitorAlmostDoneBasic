using CommunityToolkit.Mvvm.Messaging;
using CourtInvitor.Models;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of session management.
    /// </summary>
    internal class Session : SessionModel
    {
        #region Events
        /// <summary>
        /// Occurs when the remaining time changes.
        /// </summary>
        public event EventHandler? TimeLeftChanged;
        /// <summary>
        /// Occurs when the session expires.
        /// </summary>
        public event EventHandler? SessionExpired;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the remaining time display text.
        /// </summary>
        public override string TimeLeft { get; protected set; } = string.Empty;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Session class.
        /// </summary>
        public Session()
        {
            RegisterTimer();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Registers the timer for session tracking.
        /// </summary>
        public override void RegisterTimer()
        {
            WeakReferenceMessenger.Default.Register<AppMessage<long>>(this, (r, m) =>
            {
                OnMessageReceived(m.Value);
            });
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Handles timer messages.
        /// </summary>
        /// <param name="value">The timer value in milliseconds.</param>
        private void OnMessageReceived(long value)
        {
            if (value == Keys.FinishedSignal)
            {
                TimeLeft = Strings.TimeUp;
                SessionExpired?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                TimeLeft = TimeSpan
                    .FromMilliseconds(value)
                    .ToString(@"mm\:ss");
                TimeLeftChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
