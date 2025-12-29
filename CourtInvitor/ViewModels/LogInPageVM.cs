using CommunityToolkit.Mvvm.Messaging;
using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class LoginPageVM : ObservableObject
    {
        private string email = string.Empty;
        private string password = string.Empty;
        private bool isPassword = true;
        private readonly User user;
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
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public ICommand NavToRegisterCommand { get; }
        public LoginPageVM()
        {
            user = new User();
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            LoginCommand = new Command(async () => await LoginAsync());
            NavBackHomeCommand = new Command(NavigateBackHome);
            NavToRegisterCommand = new Command(NavigateToRegister);
        }
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
        }

        private async Task LoginAsync()
        {
            user.Email = Email;
            user.Password = Password;
            bool success = await user.Login();
            if (!success)
                return;
            Session session = new Session();

            WeakReferenceMessenger.Default.Send(
                new AppMessage<TimerSettings>(
                    new TimerSettings(
                        Keys.SessionTotalTime,
                        Keys.SessionInterval)));

            if (user.Role == Strings.Admin)
                    await Shell.Current.GoToAsync("///NavigataionPageAdmin");
                else
                    await Shell.Current.GoToAsync("///NavigationPageClient");
            
        }

        private async void NavigateBackHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }

        private async void NavigateToRegister()
        {
            await Shell.Current.GoToAsync("///RegisterPage");
        }
        //public ICommand NavToRegisterCommand => new Command(NavToRegister);
        //public ICommand NavBackHomeCommand => new Command(NavHome);
        //public ICommand LoginCommand { get; }
        //public ICommand ToggleIsPasswordCommand { get; }
        //private App? app;
        //private User user;
        //public bool IsBusy { get; set; } = false;
        //public string Email
        //{
        //    get => user.Email;
        //    set
        //    {
        //        user.Email = value;
        //        (LoginCommand as Command)?.ChangeCanExecute();
        //    }
        //}
        //public string Password
        //{
        //    get => user.Password;
        //    set
        //    {
        //        user.Password = value;
        //        (LoginCommand as Command)?.ChangeCanExecute();
        //    }
        //}
        //public bool IsPassword { get; set; } = true;
        //public LoginPageVM()
        //{
        //    app = Application.Current as App;
        //    user = app!.user;
        //    LoginCommand = new Command(async () => await Login());
        //    ToggleIsPasswordCommand = new Command(ToggleIsPassword);
        //}
        //private void ToggleIsPassword()
        //{
        //    IsPassword = !IsPassword;
        //    OnPropertyChanged(nameof(IsPassword));
        //}
        //private bool CanLogin()
        //{
        //    return user.CanLogin();
        //}
        //private async Task Login()
        //{


        //    //IsBusy = true;
        //    // OnPropertyChanged(nameof(IsBusy));
        //    // bool successfullyLogged = await user.Login();
        //    // IsBusy = false;
        //    // OnPropertyChanged(nameof(IsBusy));
        //    // if (successfullyLogged)
        //    //     await Shell.Current.GoToAsync("///NavigationPageClient?refresh=true");
        //    IsBusy = true;
        //    OnPropertyChanged(nameof(IsBusy));
        //    bool successfullyLogged = await user.Login();
        //    IsBusy = false;
        //    OnPropertyChanged(nameof(IsBusy));

        //    if (successfullyLogged)
        //    {
        //        if (user.Role == Strings.Admin)
        //            await Shell.Current.GoToAsync("///NavigationPageAdmin?refresh=true");
        //        else
        //            await Shell.Current.GoToAsync("///NavigationPageClient?refresh=true");
        //    }


        //}
        //public void ApplyQueryAttributes(IDictionary<string, object> query)
        //{
        //    app = Application.Current as App;
        //    user = app!.user;
        //    RefreshProperties();
        //}
        //private void RefreshProperties()
        //{
        //    IsPassword = true;
        //    SeveralPropertiesChange();
        //}
        //private void SeveralPropertiesChange()
        //{
        //    string[] nameOfs = { nameof(Email), nameof(Password), nameof(IsPassword) };
        //    for (int i = 0; i < nameOfs.Length; i++)
        //        OnPropertyChanged(nameOfs[i]);
        //}
        //private async void NavToRegister()
        //{
        //    await Shell.Current.GoToAsync("///RegisterPage?refresh=true");
        //}
        //private async void NavHome()
        //{
        //    await Shell.Current.GoToAsync("///MainPage?refresh=true");
        //}
    }
}


