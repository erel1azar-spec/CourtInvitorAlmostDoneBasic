using CommunityToolkit.Mvvm.Messaging;
using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the login page.
    /// </summary>
    internal class LoginPageVM : ObservableObject
    {
        #region Fields
        private string email = string.Empty;
        private string password = string.Empty;
        private bool isPassword = true;
        private readonly User user;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        /// <summary>
        /// Gets or sets whether password is hidden.
        /// </summary>
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                if (isPassword != value)
                {
                    isPassword = value;
                    OnPropertyChanged(nameof(IsPassword));
                }
            }
        }
        #endregion
        #region Commands
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public ICommand NavToRegisterCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the LoginPageVM class.
        /// </summary>
        public LoginPageVM()
        {
            user = new User();
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            LoginCommand = new Command(async () => await LoginAsync());
            NavBackHomeCommand = new Command(NavigateBackHome);
            NavToRegisterCommand = new Command(NavigateToRegister);
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Toggles password visibility.
        /// </summary>
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
        }
        /// <summary>
        /// Performs the login operation.
        /// </summary>
        private async Task LoginAsync()
        {
            user.Email = Email;
            user.Password = Password;
            bool success = await user.Login();
            if (success)
            {
                InitializeSession();
                if (user.Role == Strings.Admin)
                    await Shell.Current.GoToAsync("///NavigationPageAdmin");
                else
                    await Shell.Current.GoToAsync("///NavigationPageClient");
            }
        }
        /// <summary>
        /// Initializes the user session timer.
        /// </summary>
        private static void InitializeSession()
        {
            WeakReferenceMessenger.Default.Send(
                new AppMessage<TimerSettings>(
                    new TimerSettings(
                        Keys.SessionTotalTime,
                        Keys.SessionInterval)));
        }
        /// <summary>
        /// Navigates back to the home page.
        /// </summary>
        private async void NavigateBackHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }
        /// <summary>
        /// Navigates to the registration page.
        /// </summary>
        private async void NavigateToRegister()
        {
            await Shell.Current.GoToAsync("///RegisterPage");
        }
        #endregion
    }
}
