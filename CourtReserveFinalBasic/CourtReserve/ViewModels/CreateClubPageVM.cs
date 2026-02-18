using CourtReserve.Models;
using CourtReserve.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtReserve.ViewModels
{
    public class CreateClubPageVM: ObservableObject
    {
        private readonly ClubModel clubModel;
        public string ClubName
        {
            get => clubModel.ClubName;
            set
            {
                clubModel.ClubName = value;
                OnPropertyChanged(nameof(ClubName));
            }
        }
        public string Location
        {
            get => clubModel.Location;
            set
            {
                clubModel.Location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        public string Phone
        {
            get => clubModel.Phone;
            set
            {
                clubModel.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        public string Email
        {
            get => clubModel.Email;
            set
            {
                clubModel.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public int CourtsCount
        {
            get => clubModel.CourtsCount;
            set
            {
                clubModel.CourtsCount = value;
                OnPropertyChanged(nameof(CourtsCount));
            }
        }
        public string StatusMessage => clubModel.StatusMessage;
        public Color StatusColor => clubModel.IsSuccess ? Colors.LightGreen : Color.FromArgb("#FF6B6B");
        public ObservableCollection<int> CourtsNumbers { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
        public ICommand SaveCommand { get; }
        public ICommand NavBackHomeCommand { get; }
        public CreateClubPageVM() : this(new Club())
        {
        }
        public CreateClubPageVM(ClubModel createClubModel)
        {
            clubModel = createClubModel;
            SaveCommand = new Command(async () => await SaveClubAsync());
            NavBackHomeCommand = new Command(async () => await BackHomeCommand());
        }
        
        private async Task SaveClubAsync()
        {
            await clubModel.CreateClubAsync(DateTime.Today);
            OnPropertyChanged(nameof(StatusMessage));
            OnPropertyChanged(nameof(StatusColor));
        }
        private async Task BackHomeCommand()
        {
            await Shell.Current.GoToAsync("///NavigationPageAdmin?refresh=true");
        }
    }
}
