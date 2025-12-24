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
    internal class ClientExistingDatesListVM:ObservableObject
    {
        public ObservableCollection<ClientExistingDatesListModel> ClientExistingDatesListModel { get; set; } = new();
        public ICommand NavBackHomeCommand => new Command(NavHome);


        public ClientExistingDatesListVM()
        {
            LoadDates();
        }

        private async void LoadDates()
        {
            var logic = new ClientExistingDatesList();
            var dates = await logic.LoadClieDatesAsync();

            ClientExistingDatesListModel.Clear();

            foreach (var date in dates)
            {
                // בכל מודל אנו מוסיפים את ה‑Command שלו
                date.ClickCommand = new Command(() => OnDateClicked(date.DateText));
                ClientExistingDatesListModel.Add(date);
            }
        }

        private async void OnDateClicked(string selectedDate)
        {
            Preferences.Set(Keys.SelectedDate, selectedDate);
            // כל כפתור מפנה לאותו עמוד
            await Shell.Current.GoToAsync("///ClientExistingCourtsList?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///ClientExistingClubList?refresh=true");
        }
    }
}
