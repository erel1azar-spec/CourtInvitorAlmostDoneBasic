using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class HourTextModel
    {
        #region Fields
        protected int index;
        protected string timeText = string.Empty;
        #endregion
        #region Properties
        public int Index => index;
        public string TimeText => timeText;
        #endregion
        #region Protected Functions
        protected abstract string FormatHour(int hourIndex);
        #endregion
    }
}
