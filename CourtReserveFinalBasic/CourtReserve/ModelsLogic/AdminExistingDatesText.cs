using CourtReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    class AdminExistingDatesText:AdminExistingDatesTextModel
    {
        public AdminExistingDatesText(string date)
        {
            dateText = date;
        }
        public AdminExistingDatesText(string date, int freeSlots, int totalSlots)
        {
            dateText = date;
            availabilityText = $"{freeSlots}/{totalSlots} free";
        }
    }
}
