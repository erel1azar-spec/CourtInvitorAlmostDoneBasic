using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CourtReserve.Models;
using CourtReserve.ModelsLogic;

namespace CourtReserve.ViewModels
{
    internal class LoginPageVM: ObservableObject
    {
        private readonly App? app;
        private readonly User user;
        private bool isPassword;
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public bool IsPassword
        {
            get => isPassword;
            set
            {
                isPassword = value;
                OnPropertyChanged(nameof(IsPassword));
            }
        }
        
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand NavToRegisterCommand { get; }
        public LoginPageVM()
        {
            app = Application.Current as App;
            user = new User();
            isPassword = true;
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            LoginCommand = new Command(LoginAsync);
            NavToRegisterCommand = new Command(NavigateToRegister);
        }
        private void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
        }
        private async void LoginAsync()
        {
            bool success = await user.Login();
            if (success)
            {      
                await Shell.Current.GoToAsync(user.GetNavigationRoute());
            }
        }
        private async void NavigateToRegister()
        {
            await Shell.Current.GoToAsync("///RegisterPage?refresh=true");
        }
    }
}
