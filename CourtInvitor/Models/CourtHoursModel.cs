using System.Collections.ObjectModel;
namespace CourtInvitor.Models
{
    /// <summary>
    /// Abstract model for court hours management.
    /// </summary>
    public abstract class CourtHoursModel
    {
        /// <summary>
        /// Gets the collection of clients.
        /// </summary>
        public abstract ObservableCollection<Client> Clients { get; }
        /// <summary>
        /// Gets the collection of free hours.
        /// </summary>
        public abstract ObservableCollection<HourSlotModel> FreeHours { get; }
        /// <summary>
        /// Loads the free hours.
        /// </summary>
        public abstract void LoadFreeHours();
        /// <summary>
        /// Selects a specific hour.
        /// </summary>
        /// <param name="index">The hour index to select.</param>
        public abstract void SelectHour(int index);
    }
}
