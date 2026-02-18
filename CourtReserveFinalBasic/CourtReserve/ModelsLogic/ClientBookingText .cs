using CourtReserve.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.ModelsLogic
{
    public class ClientBookingText:ClientBookingTextModel
    {
        public ClientBookingText(string clubName,string date,int courtNumber, int hourIndex)
        {
            this.clubName = clubName;
            this.date = date;
            this.courtNumber = courtNumber;
            int hourValue = hourIndex + 6;
            this.hour = hourValue.ToString("00") + ":00";
        }
    }
}
