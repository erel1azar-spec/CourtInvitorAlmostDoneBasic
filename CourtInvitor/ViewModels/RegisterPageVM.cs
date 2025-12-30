using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the registration page.
    /// </summary>
    internal class RegisterPageVM : ObservableObject
    {
        #region Fields
        private readonly User user;
        private bool isPassword;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the available roles.
        /// </summary>
        public ObservableCollection<string> Roles { get; }
        /// <summary>
        /// Gets or sets the selected role.
        /// </summary>
        public string SelectedRole
        {
            get => user.Role;
            set
            {
                user.Role = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string UserName
        {
            get => user.UserName;
            set
            {
                user.UserName = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                OnPropertyChanged();
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
                isPassword = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Commands
        public ICommand RegisterCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand NavToLoginCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RegisterPageVM class.
        /// </summary>
        public RegisterPageVM()
        {
            user = new User();
            isPassword = true;
            Roles = new ObservableCollection<string>
            {
                Strings.Client,
                Strings.Admin
            };
            RegisterCommand = new Command(Register);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            NavToLoginCommand = new Command(NavToLogin);
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
        /// Performs the registration operation.
        /// </summary>
        private async void Register()
        {
            if (user.CanRegister())
            {
                bool success = await user.Register();
                Preferences.Set(Keys.UserNameKey, user.UserName);
                if (success)
                    await Shell.Current.GoToAsync("///LoginPage");
            }
        }
        /// <summary>
        /// Navigates to the login page.
        /// </summary>
        private async void NavToLogin()
        {
            await Shell.Current.GoToAsync("///LoginPage");
        }
        #endregion
    }
}
