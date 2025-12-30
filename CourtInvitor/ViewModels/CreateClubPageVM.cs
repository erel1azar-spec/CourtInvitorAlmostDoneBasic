using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace CourtInvitor.ViewModels
{
    /// <summary>
    /// ViewModel for the Create Club page.
    /// </summary>
    internal class CreateClubPageVM : ObservableObject
    {
        #region Fields
        private readonly CreateClubModel clubModel;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public string ClubName
        {
            get => clubModel.ClubName;
            set
            {
                clubModel.ClubName = value;
                OnPropertyChanged(nameof(ClubName));
            }
        }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public string Location
        {
            get => clubModel.Location;
            set
            {
                clubModel.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string Phone
        {
            get => clubModel.Phone;
            set
            {
                clubModel.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email
        {
            get => clubModel.Email;
            set
            {
                clubModel.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        /// <summary>
        /// Gets or sets the courts count.
        /// </summary>
        public int CourtsCount
        {
            get => clubModel.CourtsCount;
            set
            {
                clubModel.CourtsCount = value;
                OnPropertyChanged(nameof(CourtsCount));
            }
        }
        /// <summary>
        /// Gets the status message.
        /// </summary>
        public string StatusMessage => clubModel.StatusMessage;
        /// <summary>
        /// Gets the status message color based on success/failure.
        /// </summary>
        public Color StatusColor => clubModel.IsSuccess ? Colors.LightGreen : Color.FromArgb("#FF6B6B");
        /// <summary>
        /// Gets the available court numbers.
        /// </summary>
        public ObservableCollection<int> CourtsNumbers { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
        #endregion
        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the CreateClubPageVM class.
        /// </summary>
        public CreateClubPageVM() : this(new CreateClub())
        {
        }
        /// <summary>
        /// Initializes a new instance of the CreateClubPageVM class with a model.
        /// </summary>
        /// <param name="createClubModel">The club model.</param>
        public CreateClubPageVM(CreateClubModel createClubModel)
        {
            clubModel = createClubModel;
            SaveCommand = new Command(async () => await SaveClubAsync());
            NavBackHomeCommand = new Command(async () => await BackHomeCommand());
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Saves the club asynchronously.
        /// </summary>
        private async Task SaveClubAsync()
        {
            await clubModel.CreateClubAsync(DateTime.Today);
            OnPropertyChanged(nameof(StatusMessage));
            OnPropertyChanged(nameof(StatusColor));
        }
        /// <summary>
        /// Navigates back to the admin page.
        /// </summary>
        private async Task BackHomeCommand()
        {
            await Shell.Current.GoToAsync("///NavigationPageAdmin?refresh=true");
        }
        #endregion
    }
}
