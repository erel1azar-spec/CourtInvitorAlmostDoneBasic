using CourtInvitor.Models;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of hour slot.
    /// </summary>
    public class Hour : HourModel
    {
        #region Fields
        private int index;
        private bool isAvailable;
        private string clientName;
        private string clientId;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the hour index.
        /// </summary>
        public override int Index
        {
            get => index;
            set
            {
                index = value;
                OnPropertyChanged(nameof(Index));
                OnPropertyChanged(nameof(TimeText));
            }
        }
        /// <summary>
        /// Gets the time text.
        /// </summary>
        public override string TimeText => FormatHour(index);
        /// <summary>
        /// Gets or sets whether hour is available.
        /// </summary>
        public override bool IsAvailable
        {
            get => isAvailable;
            set
            {
                isAvailable = value;
                OnPropertyChanged(nameof(IsAvailable));
            }
        }
        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public override string ClientName
        {
            get => clientName;
            set
            {
                clientName = value;
                OnPropertyChanged(nameof(ClientName));
            }
        }
        /// <summary>
        /// Gets or sets the client ID.
        /// </summary>
        public override string ClientId
        {
            get => clientId;
            set
            {
                clientId = value;
                OnPropertyChanged(nameof(ClientId));
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Hour class.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        public Hour(int hourIndex)
        {
            index = hourIndex;
            isAvailable = true;
            clientName = string.Empty;
            clientId = string.Empty;
        }
        #endregion
        #region Protected Functions
        /// <summary>
        /// Formats hour index to time text.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>Formatted time string.</returns>
        protected override string FormatHour(int hourIndex)
        {
            int hour = ConstData.StartHour + hourIndex;
            return $"{hour:D2}:00";
        }
        #endregion
    }
}

