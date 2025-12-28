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
    internal class AdminExistsCourtsVM:ObservableObject
    {
        private readonly ObservableCollection<AdminExistsCourtsModel> courts;
        public ObservableCollection<AdminExistsCourtsModel> Courts => courts;

        public ICommand CourtSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }

        public AdminExistsCourtsVM()
        {
            courts = new ObservableCollection<AdminExistsCourtsModel>();
            CourtSelectedCommand = new Command<AdminExistsCourtsModel>(OnCourtSelected);
            NavBackHomeCommand = new Command(NavHome);
            Load();
        }

        private async void Load()
        {
            string clubName = Preferences.Get(Keys.AdminSelectedClub, string.Empty);
            var result = await AdminExistsCourts.LoadCourtsAsync(clubName);

            courts.Clear();
            foreach (AdminExistsCourtsModel model in result)
                courts.Add(model);
        }

        private void OnCourtSelected(AdminExistsCourtsModel selectedCourt)
        {
            if (selectedCourt == null)
                return;

            Preferences.Set(Keys.AdminSelectedCourt, selectedCourt.CourtNumber);
            Shell.Current.GoToAsync("///AdminExistsClients?refresh=true");
        }

        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistsClubs");
        }
        //public ObservableCollection<AdminExistsCourtsModel> AdminExistsCourtsModel { get; set; } = new();
        //public ICommand NavBackHomeCommand => new Command(NavHome);



        //public AdminExistsCourtsVM()
        //{
        //    LoadCourts();
        //}

        //private async void LoadCourts()
        //{
        //    var logic = new AdminExistsCourts();
        //    int courtsCount = await logic.GetClubNumCourtsForCurrentUserAsync();

        //    AdminExistsCourtsModel.Clear();

        //    for (int i = 1; i <= courtsCount; i++)
        //    {
        //        var model = new AdminExistsCourtsModel
        //        {
        //            DateText = $"Court {i}", // כאן הטקסט של הכפתור
        //            ClickCommand = new Command(() => OnCourtClicked(i))
        //        };
        //        AdminExistsCourtsModel.Add(model);
        //    }
        //}

        //private async void OnCourtClicked(int courtNumber)
        //{
        //    // שמירת המגרש שנבחר ב‑Preferences
        //    Preferences.Set(Keys.SelectedCourt, courtNumber);

        //    // לדוגמה, הפניה לעמוד אחר
        //    await Shell.Current.GoToAsync("///AdminExistsClients");
        //}
        //private async void NavHome()
        //{
        //    await Shell.Current.GoToAsync("///AdminExistsDates");
        //}
    }
}
