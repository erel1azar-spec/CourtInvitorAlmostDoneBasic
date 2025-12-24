using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;

namespace CourtInvitor.ViewModels
{
    internal class AdminExistsClubsVM:ObservableObject
    {
        public ICommand NavToDateCommand => new Command(NavToDate);
        public ICommand NavBackHomeCommand => new Command(NavHome);
        private readonly AdminExistsClubs modelLogic = new();
        private string name=string.Empty;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }



        public AdminExistsClubsVM()
        {
            LoadClubName();
        }

        private async void LoadClubName()
        {
            Name = await modelLogic.GetClubNameForCurrentUserAsync();
        }

        private async void NavToDate()
        {
            await Shell.Current.GoToAsync("//AdminExistsDates?refresh=true");
        }
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigataionPageAdmin?refresh=true");
        }
    }
}
