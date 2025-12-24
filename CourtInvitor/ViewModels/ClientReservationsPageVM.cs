using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class ClientReservationsPageVM
    {
        public ICommand NavBackHomeCommand => new Command(NavHome);
        private async void NavHome()
        {
            await Shell.Current.GoToAsync("///NavigationPageClient?refresh=true");
        }
    }
}
