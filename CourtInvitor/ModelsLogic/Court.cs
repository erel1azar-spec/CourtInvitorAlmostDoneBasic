using CourtInvitor.Models;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of court.
    /// </summary>
    public class Court : CourtModel
    {
        #region Fields
        private int number;
        private string clubName;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the court number.
        /// </summary>
        public override int Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
                OnPropertyChanged(nameof(DisplayText));
            }
        }
        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public override string ClubName
        {
            get => clubName;
            set
            {
                clubName = value;
                OnPropertyChanged(nameof(ClubName));
            }
        }
        /// <summary>
        /// Gets the display text for court.
        /// </summary>
        public override string DisplayText => $"{Strings.Court}{number}";
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Court class.
        /// </summary>
        public Court()
        {
            number = 0;
            clubName = string.Empty;
        }
        #endregion
    }
}

