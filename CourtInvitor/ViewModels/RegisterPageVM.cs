using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class RegisterPageVM : ObservableObject, IQueryAttributable
    {
        public ICommand NavToLoginCommand => new Command(NavToLogin);
        public ICommand NavBackHomeCommand => new Command(NavHome);
        public ICommand RegisterCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public string Role { get => user.Role; set { user.Role = value; OnPropertyChanged(); } }
        public ObservableCollection<string> Roles { get; set; } = new ObservableCollection<string> { Strings.Client, Strings.Admin };
        public string SelectedRole { get => Role; set { Role = value; OnPropertyChanged(); } }
        public bool IsBusy { get; set; } = false;
        private App? app;
        private User user;
        public string UserName
        {
            get => user.UserName;
            set
            {
                user.UserName = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
        public bool IsPassword { get; set; } = true;
        public RegisterPageVM()
        {
                app = Application.Current as App;
                user = app!.user;
                //RegisterCommand = new Command(Register, CanRegister);
                RegisterCommand = new Command(Register);
                ToggleIsPasswordCommand = new Command(ToggleIsPassword);
        }
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }
        private bool CanRegister()
        {
            return user.CanRegister();
        }
        private async void Register()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));

            user.Role = SelectedRole;

            bool successfullyRegistered = await user.Register();
            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));

            if (successfullyRegistered)
                await Shell.Current.GoToAsync("///LoginPage?refresh=true");
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            app = Application.Current as App;
            user = app!.user;
            RefreshProperties();
        }
        private void RefreshProperties()
        {
            IsPassword = true;
            SeveralPropertiesChange();
        }
        private void SeveralPropertiesChange()
        {
            string[] nameOfs = { nameof(UserName), nameof(Email), nameof(Password), nameof(IsPassword) };
            for (int i = 0; i < nameOfs.Length; i++)
                OnPropertyChanged(nameOfs[i]);
        }
        private async void NavToLogin()
        {
            await Shell.Current.GoToAsync("///LoginPage?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }
        
    }
}



