using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
