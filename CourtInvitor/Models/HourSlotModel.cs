using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    public abstract class HourSlotModel
    {
        public abstract int Index { get; }
        public abstract string TimeText { get; }
    }
}
