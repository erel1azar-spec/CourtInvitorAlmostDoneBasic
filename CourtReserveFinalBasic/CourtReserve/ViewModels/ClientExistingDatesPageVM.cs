using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CourtReserve.Models;
using CourtReserve.ModelsLogic;

namespace CourtReserve.ViewModels
{
    public  class ClientExistingDatesPageVM:ObservableObject, IQueryAttributable
    {
        private readonly DateModel model;
        public ObservableCollection<AdminExistingDatesTextModel> Dates => model.Dates;
        public ICommand DateSelectedCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public ClientExistingDatesPageVM()
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
            await model.LoadAsyncClient();
        }
        private void OnDateSelected(string selectedDate)
        {
            model.SelectDateClient(selectedDate);
            Shell.Current.GoToAsync("///ClientExistingCourtsPage?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///ClientExistingClubsPage?refresh=true");
        }
    }
}
