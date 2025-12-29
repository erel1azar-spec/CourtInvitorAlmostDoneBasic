using System.Collections.ObjectModel;

namespace CourtInvitor.Models
{
    public abstract class CourtHoursModel
    {
        public abstract ObservableCollection<Client> Clients { get; }

        public abstract ObservableCollection<HourSlotModel> FreeHours { get; }
        public abstract void LoadFreeHours();
        public abstract void SelectHour(int index);
    }
}
