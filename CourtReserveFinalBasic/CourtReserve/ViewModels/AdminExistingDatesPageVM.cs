using CommunityToolkit.Mvvm.ComponentModel;
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
    class AdminExistingDatesPageVM: IQueryAttributable
    {
        private readonly DateModel model;
        public ObservableCollection<AdminExistingDatesTextModel> Dates => model.Dates;
        public ICommand DateSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public AdminExistingDatesPageVM()
        {
            model = new Date();
            NavBackHomeCommand = new Command(NavHome);
            DateSelectedCommand = new Command<string>(OnDateSelected);
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Load();
        }
        private async void Load()
        {
            await model.LoadAsync();
        }
        private void OnDateSelected(string selectedDate)
        {
            model.SelectDate(selectedDate);
            Shell.Current.GoToAsync("///AdminExistingCourtsPage?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///AdminExistingClubsPage?refresh=true");
        }
    }
}
