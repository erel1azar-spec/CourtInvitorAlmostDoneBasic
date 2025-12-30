using CourtInvitor.Models;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of hour slot logic.
    /// </summary>
    internal class HourSlot : HourSlotModel
    {
        #region Fields
        private readonly int index;
        private readonly string timeText;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the hour index.
        /// </summary>
        public override int Index => index;
        /// <summary>
        /// Gets the time text.
        /// </summary>
        public override string TimeText => timeText;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the HourSlot class.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        public HourSlot(int hourIndex)
        {
            index = hourIndex;
            timeText = FormatHour(hourIndex);
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Formats the hour index as time text.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>The formatted time text.</returns>
        private static string FormatHour(int hourIndex)
        {
            int hour = hourIndex + 6;
            return hour.ToString("00") + ":00";
        }
        #endregion
    }
}
