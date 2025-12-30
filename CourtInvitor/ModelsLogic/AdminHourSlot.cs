using CourtInvitor.Models;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of admin hour slot logic.
    /// </summary>
    internal class AdminHourSlot : AdminHourSlotModel
    {
        #region Fields
        private readonly int index;
        private readonly string clientName;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the hour index.
        /// </summary>
        public override int Index => index;
        /// <summary>
        /// Gets the client name.
        /// </summary>
        public override string ClientName => clientName;
        /// <summary>
        /// Gets the time text for this slot.
        /// </summary>
        public override string TimeText
        {
            get
            {
                int startHour = index + 6;
                int endHour = startHour + 1;
                return $"{startHour}:00 - {endHour}:00";
            }
        }
        /// <summary>
        /// Gets the status text showing client or available.
        /// </summary>
        public override string StatusText => string.IsNullOrEmpty(clientName) ? Strings.Available : clientName;
        /// <summary>
        /// Gets whether this slot is booked.
        /// </summary>
        public override bool IsBooked => !string.IsNullOrEmpty(clientName);
        /// <summary>
        /// Gets the text color for the status.
        /// </summary>
        public override Color StatusTextColor => IsBooked ? Colors.White : Color.FromArgb("#22C55E");
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the AdminHourSlot class.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <param name="client">The client name.</param>
        public AdminHourSlot(int hourIndex, string client)
        {
            index = hourIndex;
            clientName = client;
        }
        #endregion
    }
}

