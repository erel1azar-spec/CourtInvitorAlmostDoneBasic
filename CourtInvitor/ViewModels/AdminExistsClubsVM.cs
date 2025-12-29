using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class AdminExistsClubsVM:ObservableObject
    {
        private readonly AdminExistsClubs adminExistsClubs;

        public ICommand NavToDateCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public string ClubName => adminExistsClubs.Name;
        public AdminExistsClubsVM()
        {
            adminExistsClubs = new AdminExistsClubs();
            NavToDateCommand = new Command(NavToDate);
            NavBackHomeCommand = new Command(NavHome);
            Load();
        }
        private async void Load()
        {
            string email =
                Preferences.Get(Keys.EmailKey,string.Empty);

            if (email != string.Empty)
            {
                await adminExistsClubs.LoadByUserEmailAsync(email);
                OnPropertyChanged(nameof(ClubName));
                Preferences.Set(Keys.AdminSelectedClub,ClubName);
            }
        }

        private async void NavToDate()
        {
            await Shell.Current.GoToAsync("//AdminExistsDates?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigataionPageAdmin?refresh=true");
        }
        //public ICommand NavToDateCommand => new Command(NavToDate);
        //public ICommand NavBackHomeCommand => new Command(NavHome);
        //private readonly AdminExistsClubs modelLogic = new();
        //private string name=string.Empty;
        //public string Name
        //{
        //    get => name;
        //    set
        //    {
        //        name = value;
        //        OnPropertyChanged();
        //    }
        //}



        //public AdminExistsClubsVM()
        //{
        //    LoadClubName();
        //}

        //private async void LoadClubName()
        //{
        //    Name = await modelLogic.GetClubNameForCurrentUserAsync();
        //}

        //private async void NavToDate()
        //{
        //    await Shell.Current.GoToAsync("//AdminExistsDates?refresh=true");
        //}
        //private async void NavHome()
        //{
        //    await Shell.Current.GoToAsync("///NavigataionPageAdmin?refresh=true");
        //}
    }
}
