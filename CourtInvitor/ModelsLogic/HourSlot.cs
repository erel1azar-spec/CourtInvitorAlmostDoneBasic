using CourtInvitor.Models;

namespace CourtInvitor.ModelsLogic
    {
        internal class HourSlot:HourSlotModel
        {
            private readonly int index;
            private readonly string timeText;

            public override int Index => index;
            public override string TimeText => timeText;

            public HourSlot(int hourIndex)
            {
                index = hourIndex;
                timeText = FormatHour(hourIndex);
            }

            private static string FormatHour(int hourIndex)
            {
                int hour = hourIndex + 6;
                return hour.ToString("00") + ":00";
            }
        }
    }
