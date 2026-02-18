using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class AdminExistingCourtsTextModel
    {
        #region Fields
        protected int courtNumber;
        protected string courtText = string.Empty;
        protected string availabilityText = string.Empty;
        #endregion
        #region Properties
        public int CourtNumber => courtNumber;
        public string CourtText => courtText;
        public string AvailabilityText => availabilityText;
        #endregion
    }
}
