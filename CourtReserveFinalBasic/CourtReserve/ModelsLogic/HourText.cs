using CourtReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class HourText:HourTextModel
    {
        #region Constructor
        public HourText(int hourIndex)
        {
            index = hourIndex;
            timeText = FormatHour(hourIndex);
        }
        #endregion
        #region Protected Functions
        protected override string FormatHour(int hourIndex)
        {
            int hour = hourIndex + 6;
            return hour.ToString("00") + ":00";
        }
        #endregion
    }
}
