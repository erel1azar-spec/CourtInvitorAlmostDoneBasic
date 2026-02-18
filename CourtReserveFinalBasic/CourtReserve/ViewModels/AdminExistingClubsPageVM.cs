using CourtReserve.Models;
using CourtReserve.ModelsLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtReserve.ViewModels
{
    public class AdminExistingClubsPageVM: ObservableObject, IQueryAttributable
    {
        private readonly ClubsModel model;
        public ObservableCollection<AdminExistingClubsTextModel> Clubs => model.Clubs;
        public ICommand ClubSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public AdminExistingClubsPageVM()
        {
            model = new Clubs();
            NavBackHomeCommand = new Command(NavHome);
            ClubSelectedCommand = new Command<string>(OnClubSelected);
        }

        private void OnClubSelected(string selectedClub)
        {
            model.SelectClub(selectedClub);
            Shell.Current.GoToAsync("///AdminExistingDatesPage?refresh=true");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Load();
        }
        private async void Load()
        {
            await model.LoadAsync();
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigationPageAdmin?refresh=true");
        }
    }
}
