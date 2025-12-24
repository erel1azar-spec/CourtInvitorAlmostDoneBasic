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
    internal class AdminExistsDatesVM:ObservableObject
    {
        //public ObservableCollection<AdminExistsDates> ButtonsTexts= new ObservableCollection<AdminExistsDates>();
        //public ICommand ClickCommend => new Command(Click);
        //public string dateText = string.Empty;
        //public string DateText
        //{
        //    get => dateText;
        //    set
        //    {
        //        dateText = value;
        //        OnPropertyChanged(nameof(DateText));
        //    }
        //}
        //public AdminExistsDatesVM()
        //{
        //    LoadClubDates();
        //}

        //private void LoadClubDates()
        //{
        //    DateText= await AdminExistsDates.GetClubNameForCurrentUserAsync();
        //}

        //private async void Click()
        //{
        //    await Shell.Current.GoToAsync("///MainPage?refresh=true");
        //}

        public ObservableCollection<AdminExistsDatesModel> AdminExistsDatesModel { get; set; } = new();
        public ICommand NavBackHomeCommand => new Command(NavHome);


        public AdminExistsDatesVM()
        {
            LoadDates();
        }

        private async void LoadDates()
        {
            var logic = new AdminExistsDates();
            var dates = await logic.LoadDatesAsync();

            AdminExistsDatesModel.Clear();

            foreach (var date in dates)
            {
                // בכל מודל אנו מוסיפים את ה‑Command שלו
                date.ClickCommand = new Command(() => OnDateClicked(date.DateText));
                AdminExistsDatesModel.Add(date);
            }
        }

        private async void OnDateClicked(string selectedDate)
        {
            Preferences.Set(Keys.SelectedDate, selectedDate);
            // כל כפתור מפנה לאותו עמוד
            await Shell.Current.GoToAsync("///AdminExistsCourts?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistsClubs");
        }
    }
}
