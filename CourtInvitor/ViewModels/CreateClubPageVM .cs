using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class CreateClubPageVM: ObservableObject
    {
        public CreateClub Model { get; }
        public ICommand NavBackHomeCommand => new Command(NavHome);
        public ObservableCollection<int> CourtsNumbers { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
        public bool IsBusy { get; set; } = false;
        public CreateClubPageVM()
        {
            Model = new CreateClub();
            SaveCommand = new Command(async () => await SaveClub());
            string savedName = Preferences.Get(Keys.ClubName, string.Empty);
            if (!string.IsNullOrEmpty(savedName))
            {
                ClubName = savedName;
                OnPropertyChanged(nameof(ClubName));
            }
        }

        public string ClubName
        {
            get => Model.ClubName;
            set => Model.ClubName = value;
        }
        public string Location
        {
            get => Model.Location;
            set => Model.Location = value;
        }

        public string Phone
        {
            get => Model.Phone;
            set => Model.Phone = value;
        }

        public string Email
        {
            get => Model.Email;
            set => Model.Email = value;
        }

        public int CourtsCount
        {
            get => Model.CourtsCount;
            set => Model.CourtsCount = value;
        }

        public string StatusMessage => Model.StatusMessage;

        public ICommand SaveCommand { get; }

        private async Task SaveClub()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            DateTime startDate = DateTime.Today;
            bool successfullyLogged = await Model.CreateClubAsync(startDate, t => { });
            if (successfullyLogged)
            {
                Preferences.Set(Keys.ClubName, ClubName);
            }
            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));

            
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }
    }
}

