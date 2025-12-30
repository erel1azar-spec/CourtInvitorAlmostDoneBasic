using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the main page.
    /// </summary>
    internal class MainPageVM : ObservableObject, IQueryAttributable
    {
        #region Fields
        private readonly App? app;
        private readonly User user;
        private bool isLogged;
        private string? welcomeUserName;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets whether user is logged in.
        /// </summary>
        private bool IsLogged
        {
            get => isLogged;
            set
            {
                if (isLogged != value)
                {
                    isLogged = value;
                    SeveralPropertiesChange();
                }
            }
        }
        /// <summary>
        /// Gets whether sign out button is visible.
        /// </summary>
        public bool SignOutVisibility => IsLogged;
        /// <summary>
        /// Gets whether login button is visible.
        /// </summary>
        public bool LoginVisibility => !IsLogged;
        /// <summary>
        /// Gets or sets the welcome message.
        /// </summary>
        public string? WelcomeUserName
        {
            get => welcomeUserName;
            set
            {
                if (welcomeUserName != value)
                {
                    welcomeUserName = value;
                    OnPropertyChanged(nameof(WelcomeUserName));
                }
            }
        }
        #endregion
        #region Commands
        public ICommand NavToLoginCommand { get => new Command(NavToLogin); }
        public ICommand SignOutCommand { get => new Command(SignOut); }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MainPageVM class.
        /// </summary>
        public MainPageVM()
        {
            app = Application.Current as App;
            user = app!.user;
            RefreshProperties();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Applies query attributes when navigating to this page.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            RefreshProperties();
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Notifies multiple properties have changed.
        /// </summary>
        private void SeveralPropertiesChange()
        {
            string[] nameOfs = { nameof(WelcomeUserName), nameof(LoginVisibility), nameof(SignOutVisibility) };
            for (int i = 0; i < nameOfs.Length; i++)
                OnPropertyChanged(nameOfs[i]);
        }
        /// <summary>
        /// Refreshes display properties.
        /// </summary>
        private void RefreshProperties()
        {
            WelcomeUserName = $"{Strings.Welcome} {Preferences.Get(Keys.UserNameKey, Strings.Guest)}!";
            IsLogged = Preferences.Get(Keys.EmailKey, string.Empty) != string.Empty;
        }
        /// <summary>
        /// Navigates to the login page.
        /// </summary>
        private async void NavToLogin()
        {
            await Shell.Current.GoToAsync("///LoginPage?refresh=true");
        }
        /// <summary>
        /// Signs out the current user.
        /// </summary>
        private void SignOut()
        {
            user.SignOut();
            app!.user = new User();
            RefreshProperties();
        }
        #endregion
    }
}
