using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class AdminExistsDatesVM : ObservableObject
    {


        //public ObservableCollection<AdminExistsDatesModel> AdminExistsDatesModel { get; set; } = new();
        //public ICommand NavBackHomeCommand => new Command(NavHome);


        //public AdminExistsDatesVM()
        //{
        //    LoadDates();
        //}

        //private async void LoadDates()
        //{
        //    var logic = new AdminExistsDates();
        //    var dates = await logic.LoadDatesAsync();

        //    AdminExistsDatesModel.Clear();

        //    foreach (var date in dates)
        //    {
        //        // בכל מודל אנו מוסיפים את ה‑Command שלו
        //        date.ClickCommand = new Command(() => OnDateClicked(date.DateText));
        //        AdminExistsDatesModel.Add(date);
        //    }
        //}

        //private async void OnDateClicked(string selectedDate)
        //{
        //    Preferences.Set(Keys.SelectedDate, selectedDate);
        //    // כל כפתור מפנה לאותו עמוד
        //    await Shell.Current.GoToAsync("///AdminExistsCourts?refresh=tru e");
        //}
        public ICommand DateSelectedCommand { get; }

        private readonly ObservableCollection<AdminExistsDatesModel>
           dates;

        public ObservableCollection<AdminExistsDatesModel>
            Dates => dates;

        public ICommand NavBackHomeCommand { get; }

        public AdminExistsDatesVM()
        {
            dates =
                new ObservableCollection<AdminExistsDatesModel>();

            NavBackHomeCommand =
                new Command(NavHome);

            DateSelectedCommand=
                new Command<string>(OnDateSelected);

            Load();
        }

        private async void Load()
        {
            string clubName =
                Preferences.Get(Keys.AdminSelectedClub, string.Empty);

            List<AdminExistsDatesModel> result =
                await AdminExistsDates
                .LoadDatesAsync(clubName);

            dates.Clear();

            foreach (AdminExistsDatesModel model in result)
                dates.Add(model);
            
        }
        private void OnDateSelected(string selectedDate)
        {
            Preferences.Set(Keys.AdminSelectedDate, selectedDate);

            Shell.Current.GoToAsync("///AdminExistsCourts?refresh=true");
        }

        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistsClubs");
        }
    }
}
