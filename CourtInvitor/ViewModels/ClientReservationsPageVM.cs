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
