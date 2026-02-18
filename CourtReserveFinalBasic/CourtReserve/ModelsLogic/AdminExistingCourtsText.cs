using CourtReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class AdminExistingCourtsText:AdminExistingCourtsTextModel
    {
        #region Constructor
        public AdminExistingCourtsText(int number)
        {
            courtNumber = number;
            courtText = Strings.Court + number;
        }
        public AdminExistingCourtsText(int number, int freeSlots, int totalSlots)
        {
            courtNumber = number;
            courtText = Strings.Court + number;
            availabilityText = $"{freeSlots}/{totalSlots} free";
        }
        #endregion
    }
}
