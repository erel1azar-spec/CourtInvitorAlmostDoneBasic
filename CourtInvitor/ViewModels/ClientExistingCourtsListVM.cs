using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class ClientExistingCourtsListVM: ObservableObject
    {
        private readonly ObservableCollection<ClientExistingCourtsListModel> courts;
        public ObservableCollection<ClientExistingCourtsListModel> Courts => courts;

        public ICommand CourtSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }

        public ClientExistingCourtsListVM()
        {
            courts = new();
            CourtSelectedCommand = new Command<ClientExistingCourtsListModel>(OnCourtSelected);
            NavBackHomeCommand = new Command(NavHome);
            Load();
        }

        private async void Load()
        {
            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
            var result = await ClientExistingCourtsList.LoadCourtsAsync(clubName);

            courts.Clear();
            foreach (ClientExistingCourtsListModel model in result)
                courts.Add(model);
        }

        private void OnCourtSelected(ClientExistingCourtsListModel selectedCourt)
        {
            Preferences.Set(Keys.ClientSelectedCourt, selectedCourt.CourtNumber);
            Shell.Current.GoToAsync("///ClientExistingHoursPage?refresh=true");
        }

        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///ClientExistingDatesList?refresh=true");
        }
    }
}
