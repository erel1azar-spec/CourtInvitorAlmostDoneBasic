using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class ProfileClientPageVM:ObservableObject
    {
        readonly Profiles profiles = new();
         readonly UserProfile userProfile = new();
        public bool IsBusy => profiles.IsBusy;
        public ObservableCollection<Profile>? ProfileDatas { get => profiles.ProfileDatas; set => profiles.ProfileDatas = value; }
        public ICommand AddDataCommand => new Command(AddData);
        public ICommand NavBackHomeCommand => new Command(NavHome);

        private void AddData()
        {
            profiles.AddProfile();
            OnPropertyChanged(nameof(IsBusy));
        }
        public ObservableCollection<Profile>? ProfileList => profiles.ProfileList;

        public ProfileClientPageVM()
        {
            profiles.OnProfileAdded += OnProfileAdded;
            profiles.OnProfilesChanged += OnProfilesChanged;
        }
        private void OnProfilesChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ProfileList));
        }
        private void OnProfileAdded(object? sender, bool e)
        {
            OnPropertyChanged(nameof(IsBusy));
        }
        internal void AddSnapshotListener()
        {
            profiles.AddSnapshotListener();
        }
        internal void RemoveSnapshotListener()
        {
            profiles.RemoveSnapshotListener();
        }
        public string FirstName
        {
            get => userProfile.FirstName;
            set
            {
                userProfile.FirstName = value;
                
            }
        }
        public string LastName
        {
            get => userProfile.LastName;
            set
            {
                userProfile.LastName = value;
                
            }
        }
        public string City
        {
            get => userProfile.City;
            set
            {
                userProfile.City = value;
                
            }
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigationPageClient?refresh=true");
        }
    }
}
