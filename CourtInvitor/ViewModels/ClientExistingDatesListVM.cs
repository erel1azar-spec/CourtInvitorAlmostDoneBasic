using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class ClientExistingDatesListVM:ObservableObject
    {
        public ICommand DateSelectedCommand { get; }

        private readonly ObservableCollection<ClientExistingDatesListModel>
           dates;

        public ObservableCollection<ClientExistingDatesListModel>
            DatesClient => dates;

        public ICommand NavBackHomeCommand { get; }

        public ClientExistingDatesListVM()
        {
            dates =
                new ObservableCollection<ClientExistingDatesListModel>();

            NavBackHomeCommand =
                new Command(NavHome);

            DateSelectedCommand =
                new Command<string>(OnDateSelected);

            Load();
        }

        private async void Load()
        {


            string clubName = Preferences.Get(Keys.ClientSelectedClub, string.Empty);
;

            List<ClientExistingDatesListModel> result = await ClientExistingDatesList.LoadClientDatesAsync(clubName);

            dates.Clear();
            foreach (ClientExistingDatesListModel model in result)
                dates.Add(model);

        }
        private void OnDateSelected(string selectedDate)
        {
            Preferences.Set(Keys.ClientSelectedDate, selectedDate);

            Shell.Current.GoToAsync("///ClientExistingCourtsList?refresh=true");

        }

        private async void NavHome()
        {
            Preferences.Clear(Keys.ClientSelectedClub);
            await Shell.Current.GoToAsync("///ClientExistingClubList?refresh=true");
        }
    }
}
