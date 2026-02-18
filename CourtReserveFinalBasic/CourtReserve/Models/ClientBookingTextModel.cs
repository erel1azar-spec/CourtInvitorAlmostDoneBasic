using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class ClientBookingTextModel
    {
        protected string clubName = string.Empty;
        protected string date = string.Empty;
        protected int courtNumber;
        protected string hour = string.Empty;
        public string CourtText => "מגרש: " + courtNumber;
        public string HourText => "שעה: " + hour;

        public string ClubName => clubName;
        public string Date => date;
        public int CourtNumber => courtNumber;
        public string Hour => hour;
    }
}
