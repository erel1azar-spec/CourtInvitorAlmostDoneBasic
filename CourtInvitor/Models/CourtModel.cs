using System.Collections.ObjectModel;
namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for court.
    /// </summary>
    public abstract class CourtModel : ObservableObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the court number.
        /// </summary>
        public abstract int Number { get; set; }
        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public abstract string ClubName { get; set; }
        /// <summary>
        /// Gets the display text for court.
        /// </summary>
        public abstract string DisplayText { get; }
        #endregion
    }
}

