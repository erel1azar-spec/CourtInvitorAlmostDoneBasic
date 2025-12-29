using CourtInvitor.Models;
using CourtInvitor.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CourtInvitor.ViewModels
{
    internal class CreateClubPageVM: ObservableObject
    {     
        private readonly CreateClubModel clubModel;



        public ICommand SaveCommand { get; }
        public ICommand NavBackHomeCommand { get; }
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

        public ObservableCollection<int> CourtsNumbers { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
        public CreateClubPageVM() : this(new CreateClub())
        {
        }
        public CreateClubPageVM(CreateClubModel createClubModel)
        {
            clubModel = createClubModel;
            SaveCommand = new Command(async () => await SaveClubAsync());
            NavBackHomeCommand = new Command(async () => await BackHomeCommand());
        }
        private async Task SaveClubAsync()
        {
            await clubModel.CreateClubAsync(DateTime.Today);
            OnPropertyChanged(nameof(StatusMessage));
        }
        private async Task BackHomeCommand()
        {
            await Shell.Current.GoToAsync("///MainPage?refresh=true");
        }
        //public CreateClub Model { get; }
        //public ICommand NavBackHomeCommand => new Command(NavHome);
        //public ObservableCollection<int> CourtsNumbers { get; } = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6 };
        //public bool IsBusy { get; set; } = false;
        //public CreateClubPageVM()
        //{
        //    Model = new CreateClub();
        //    SaveCommand = new Command(async () => await SaveClub());
        //    string savedName = Preferences.Get(Keys.ClubName, string.Empty);
        //    if (!string.IsNullOrEmpty(savedName))
        //    {
        //        ClubName = savedName;
        //        OnPropertyChanged(nameof(ClubName));
        //    }
        //}

        //public string ClubName
        //{
        //    get => Model.ClubName;
        //    set => Model.ClubName = value;
        //}
        //public string Location
        //{
        //    get => Model.Location;
        //    set => Model.Location = value;
        //}

        //public string Phone
        //{
        //    get => Model.Phone;
        //    set => Model.Phone = value;
        //}

        //public string Email
        //{
        //    get => Model.Email;
        //    set => Model.Email = value;
        //}

        //public int CourtsCount
        //{
        //    get => Model.CourtsCount;
        //    set => Model.CourtsCount = value;
        //}

        //public string StatusMessage => Model.StatusMessage;

        //public ICommand SaveCommand { get; }

        //private async Task SaveClub()
        //{
        //    IsBusy = true;
        //    OnPropertyChanged(nameof(IsBusy));
        //    DateTime startDate = DateTime.Today;
        //    bool successfullyLogged = await Model.CreateClubAsync(startDate, t => { });
        //    if (successfullyLogged)
        //    {
        //        Preferences.Set(Keys.ClubName, ClubName);
        //    }
        //    IsBusy = false;
        //    OnPropertyChanged(nameof(IsBusy));


        //}
        //private async void NavHome()
        //{
        //    await Shell.Current.GoToAsync("///MainPage?refresh=true");
        //}
    }
}

