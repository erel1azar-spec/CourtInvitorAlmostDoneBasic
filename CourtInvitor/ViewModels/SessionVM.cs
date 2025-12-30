using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for session management.
    /// </summary>
    internal class SessionVM : ObservableObject
    {
        #region Fields
        private Session? session;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the remaining time display text.
        /// </summary>
        public string TimeLeft => session?.TimeLeft ?? string.Empty;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the SessionVM class.
        /// </summary>
        public SessionVM()
        {
            session = new Session();
            session.TimeLeftChanged += (_, _) =>
                OnPropertyChanged(nameof(TimeLeft));
            session.SessionExpired += async (_, _) =>
            {
                session = null;
                new User().SignOut();
                await Shell.Current.GoToAsync("///LoginPage");
            };
        }
        #endregion
    }
}
