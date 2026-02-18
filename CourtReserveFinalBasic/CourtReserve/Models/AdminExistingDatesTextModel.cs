using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class AdminExistingDatesTextModel
    {
        protected string dateText = string.Empty;
        protected string availabilityText = string.Empty;
        public string DateText => dateText;
        public string AvailabilityText => availabilityText;
    }
}
