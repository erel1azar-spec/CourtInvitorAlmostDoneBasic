using CourtInvitor.Models;
using System.Collections.ObjectModel;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of court date schedule.
    /// </summary>
    public class Date : DateModel
    {
        #region Fields
        private string date;
        private int courtNumber;
        private ObservableCollection<HourModel> hours;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the date string.
        /// </summary>
        public override string Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(DisplayText));
            }
        }
        /// <summary>
        /// Gets or sets the court number.
        /// </summary>
        public override int CourtNumber
        {
            get => courtNumber;
            set
            {
                courtNumber = value;
                OnPropertyChanged(nameof(CourtNumber));
                OnPropertyChanged(nameof(DisplayText));
            }
        }
        /// <summary>
        /// Gets the collection of hours for this date.
        /// </summary>
        public override ObservableCollection<HourModel> Hours => hours;
        /// <summary>
        /// Gets the display text for date.
        /// </summary>
        public override string DisplayText => $"{Strings.Court}{courtNumber} - {date}";
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Date class.
        /// </summary>
        public Date()
        {
            date = string.Empty;
            courtNumber = 0;
            hours = new ObservableCollection<HourModel>();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Checks if specific hour is available.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>True if hour is available.</returns>
        public override bool IsHourAvailable(int hourIndex)
        {
            if (hourIndex >= 0 && hourIndex < hours.Count)
                return hours[hourIndex].IsAvailable;
            return false;
        }
        /// <summary>
        /// Gets client name for specific hour.
        /// </summary>
        /// <param name="hourIndex">The hour index.</param>
        /// <returns>Client name or empty string.</returns>
        public override string GetClientAtHour(int hourIndex)
        {
            if (hourIndex >= 0 && hourIndex < hours.Count)
                return hours[hourIndex].ClientName;
            return string.Empty;
        }
        /// <summary>
        /// Adds hour to collection.
        /// </summary>
        /// <param name="hour">Hour to add.</param>
        public void AddHour(HourModel hour)
        {
            hours.Add(hour);
        }
        #endregion
    }
}

