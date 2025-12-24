using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    public abstract class CourtHourModel
    {
        public abstract string HourText { get; set; }
        public abstract string ClientId { get; set; }
        public abstract string DisplayText { get; }
    }
}
