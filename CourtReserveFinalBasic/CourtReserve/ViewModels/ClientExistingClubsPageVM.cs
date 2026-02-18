using CourtReserve.Models;
using CourtReserve.ModelsLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtReserve.ViewModels
{
    public class ClientExistingClubsPageVM : ObservableObject, IQueryAttributable
    {
        private readonly ClubsModel model;

        public ObservableCollection<AdminExistingClubsTextModel> Clubs => model.Clubs;
        public ObservableCollection<string> DateOptions { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> HourOptions { get; } = new ObservableCollection<string>();

        private string selectedDate = string.Empty;
        public string SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private string selectedHour = string.Empty;
        public string SelectedHour
        {
            get => selectedHour;
            set
            {
                selectedHour = value;
                OnPropertyChanged(nameof(SelectedHour));
            }
        }

        public ICommand ClubSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public ICommand FilterCommand { get; }
        public ICommand ShowAllCommand { get; }

        public ClientExistingClubsPageVM()
        {
            model = new Clubs();
            NavBackHomeCommand = new Command(NavHome);
            ClubSelectedCommand = new Command<string>(OnClubSelected);
            FilterCommand = new Command(OnFilter);
            ShowAllCommand = new Command(OnShowAll);

            foreach (string date in model.GetDateOptions())
                DateOptions.Add(date);
            foreach (string hour in model.GetHourOptions())
                HourOptions.Add(hour);
        }

        private void OnClubSelected(string selectedClub)
        {
            model.SelectClubClient(selectedClub);
            Shell.Current.GoToAsync("///ClientExistingDatesPage?refresh=true");
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Load();
        }

        private async void Load()
        {
            await model.LoadAsyncClient();
        }

        private async void OnFilter()
        {
            if (!string.IsNullOrEmpty(selectedDate) && !string.IsNullOrEmpty(selectedHour))
            {
                int hourIndex = model.HourTextToIndex(selectedHour);
                if (hourIndex >= 0)
                {
                    await model.FilterClubsByAvailabilityAsync(selectedDate, hourIndex);
                }
            }
        }

        private void OnShowAll()
        {
            SelectedDate = string.Empty;
            SelectedHour = string.Empty;
            Load();
        }

        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigationPageClient?refresh=true");
        }
    }
}
