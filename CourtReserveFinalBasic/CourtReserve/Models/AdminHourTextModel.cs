using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class AdminHourTextModel
    {
        #region Fields
        protected int index;
        protected string clientName = string.Empty;
        #endregion
        #region Properties
        public int Index => index;
        public string ClientName => clientName;
        public abstract string TimeText { get; }
        public abstract string StatusText { get; }
        public abstract bool IsBooked { get; }
        public abstract Color StatusTextColor { get; }
        #endregion
    }
}
