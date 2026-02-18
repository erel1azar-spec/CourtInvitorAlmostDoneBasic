using CourtReserve.Models;
using CourtReserve.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtReserve.ViewModels
{
    internal class RegisterPageVM: ObservableObject
    {
        private readonly User user;
        private bool isPassword;
        public ObservableCollection<string> Roles { get; }
        public string SelectedRole
        {
            get => user.Role;
            set
            {
                user.Role = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get => user.UserName;
            set
            {
                user.UserName = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                OnPropertyChanged();
            }
        }
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged();
            }
        }
        public ICommand RegisterCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand NavToLoginCommand { get; }
        /// Initializes a new instance of the RegisterPageVM class.
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
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
        }
        /// Performs the registration operation.
        private async void Register()
        {
            if (user.CanRegister())
            {
                bool success = await user.Register();
                if (success)
                    await Shell.Current.GoToAsync("///LoginPage");
            }
        }
        private async void NavToLogin()
        {
            await Shell.Current.GoToAsync("///LoginPage");
        }
    }
}
