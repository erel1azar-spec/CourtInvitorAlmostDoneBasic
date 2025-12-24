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
    internal class ClientExistingClubListVM:ObservableObject
    {
        public ICommand ClickCommand => new Command(Click);
        public ICommand NavBackHomeCommand => new Command(NavHome);
        private readonly AdminExistsClubs modelLogic = new();
        private string name = string.Empty;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<ClientExistingClubListModel> ClientExistingClubListModel { get; set; } = new();

        public ClientExistingClubListVM()
        {
            LoadClubName();
        }

        private async void LoadClubName()
        {
            var logic = new ClientExistingClubList();
            var names = await logic.LoadClubNamesAsync();

            ClientExistingClubListModel.Clear();

            foreach (var name in names)
            {
                // בכל מודל אנו מוסיפים את ה‑Command שלו
                name.ClickCommand = new Command(() => OnDateClicked(name.ClubText));
                ClientExistingClubListModel.Add(name);
            }
        }

        private async void OnDateClicked(string selectedClub)
        {
            Preferences.Set(Keys.SelectedClientClub, selectedClub);
            // כל כפתור מפנה לאותו עמוד
            await Shell.Current.GoToAsync("///ClientExistingDatesList?refresh=true");
        }

        private async void Click()
        {
            await Shell.Current.GoToAsync("//ClientExistingDatesList");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigationPageClient?refresh=true");
        }
    }
}
