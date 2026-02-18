using CourtReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class AdminHourText:AdminHourTextModel
    {
        #region Properties
        public override string TimeText
        {
            get
            {
                int startHour = index + 6;
                int endHour = startHour + 1;
                return $"{startHour}:00 - {endHour}:00";
            }
        }
        public override string StatusText => string.IsNullOrEmpty(clientName) ? Strings.Available : clientName;
        public override bool IsBooked => !string.IsNullOrEmpty(clientName);
        public override Color StatusTextColor => IsBooked ? Colors.White : Color.FromArgb("#22C55E");
        #endregion
        #region Constructor
        public AdminHourText(int hourIndex, string client)
        {
            index = hourIndex;
            clientName = client;
        }
        #endregion
    }
}
