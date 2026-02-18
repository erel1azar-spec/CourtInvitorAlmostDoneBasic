using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class AdminExistingClubsTextModel
    {
        protected string clubText = string.Empty;
        protected string availabilityText = string.Empty;
        public string ClubText => clubText;
        public string AvailabilityText => availabilityText;
    }
}
