using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourtReserve.Models;

namespace CourtReserve.ModelsLogic
{
    class AdminExistingClubsText: AdminExistingClubsTextModel
    {
        public AdminExistingClubsText(string club)
        {
            clubText = club;
        }
        public AdminExistingClubsText(string club, int freeSlots, int totalSlots)
        {
            clubText = club;
            availabilityText = $"{freeSlots}/{totalSlots} free";
        }
    }
}
