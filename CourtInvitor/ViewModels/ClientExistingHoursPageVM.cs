using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class ClientExistingHoursPageVM
    {
        private readonly CourtHoursModel model;

        public ICommand HourSelectedCommand { get; }
        public ICommand NavBackHomeCommand => new Command(NavHome);


        public ObservableCollection<HourSlotModel> FreeHours => model.FreeHours;

        public ClientExistingHoursPageVM()
        {
            model = new CourtHours();
            HourSelectedCommand = new Command<int>(SelectHour);
            model.LoadFreeHours();
        }

        private void SelectHour(int index)
        {
            model.SelectHour(index);
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///ClientExistingCourtsList?refresh=true");
        }
    }
}
