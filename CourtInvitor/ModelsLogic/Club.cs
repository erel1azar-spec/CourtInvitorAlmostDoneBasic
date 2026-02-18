using CourtInvitor.Models;
using Plugin.CloudFirestore;
using System.Collections.ObjectModel;
namespace CourtInvitor.ModelsLogic
{
    /// <summary>
    /// Implementation of sports club.
    /// </summary>
    public class Club : ClubModel
    {
        #region Fields
        private string name;
        private string location;
        private string phone;
        private string email;
        private int courtsCount;
        private ObservableCollection<CourtModel> courts;
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public override string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public override string Location
        {
            get => location;
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public override string Phone
        {
            get => phone;
            set
            {
                phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public override string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        /// <summary>
        /// Gets or sets the number of courts.
        /// </summary>
        public override int CourtsCount
        {
            get => courtsCount;
            set
            {
                courtsCount = value;
                OnPropertyChanged(nameof(CourtsCount));
            }
        }
        /// <summary>
        /// Gets the collection of courts.
        /// </summary>
        public override ObservableCollection<CourtModel> Courts => courts;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Club class.
        /// </summary>
        public Club()
        {
            name = string.Empty;
            location = string.Empty;
            phone = string.Empty;
            email = string.Empty;
            courtsCount = 0;
            courts = new ObservableCollection<CourtModel>();
        }
        #endregion
        #region Public Functions
        /// <summary>
        /// Initializes courts for the club.
        /// </summary>
        /// <param name="startDate">Start date for scheduling.</param>
        public override async Task InitializeCourtsAsync(DateTime startDate)
        {
            courts.Clear();
            for (int i = 1; i <= courtsCount; i++)
                courts.Add(new Court { Number = i, ClubName = name });
            await Task.CompletedTask;
        }
        /// <summary>
        /// Gets courts schedule for specific date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>Collection of dates with court schedules.</returns>
        public override async Task<ObservableCollection<DateModel>> GetCourtsForDateAsync(string date)
        {
            ObservableCollection<DateModel> courtDates = new ObservableCollection<DateModel>();
            FbData fbData = new FbData();
            for (int courtNum = 1; courtNum <= courtsCount; courtNum++)
            {
                string docId = $"{courtNum}_{date}";
                IDocumentSnapshot doc = await fbData.fs.Collection(name).Document(docId).GetAsync();
                if (doc.Exists)
                    AddDateFromDocument(doc, courtDates);
            }
            return courtDates;
        }
        #endregion
        #region Private Functions
        /// <summary>
        /// Adds date from Firestore document.
        /// </summary>
        /// <param name="document">Firestore document.</param>
        /// <param name="courtDates">Collection to add to.</param>
        private static void AddDateFromDocument(IDocumentSnapshot document, ObservableCollection<DateModel> courtDates)
        {
            System.Collections.Generic.IDictionary<string, object>? data = document.Data;
            if (data != null)
            {
                Date dateModel = new Date
                {
                    Date = data.ContainsKey("date") ? data["date"]?.ToString() ?? string.Empty : string.Empty,
                    CourtNumber = data.ContainsKey("CourtNumber") ? Convert.ToInt32(data["CourtNumber"]) : 0
                };
                if (data.ContainsKey("Lclients"))
                    ExtractHoursFromData(data["Lclients"], dateModel);
                courtDates.Add(dateModel);
            }
        }
        /// <summary>
        /// Extracts hours from client data.
        /// </summary>
        /// <param name="clientsData">Clients data object.</param>
        /// <param name="dateModel">Date model to add hours to.</param>
        private static void ExtractHoursFromData(object? clientsData, Date dateModel)
        {
            System.Collections.IList? clientsList = clientsData as System.Collections.IList;
            if (clientsList != null)
            {
                for (int i = 0; i < clientsList.Count; i++)
                {
                    System.Collections.Generic.IDictionary<string, object>? clientData = clientsList[i] as System.Collections.Generic.IDictionary<string, object>;
                    string clientName = string.Empty;
                    string clientId = string.Empty;
                    bool isAvailable = true;
                    if (clientData != null)
                    {
                        clientName = clientData.ContainsKey("Name") ? clientData["Name"]?.ToString() ?? string.Empty : string.Empty;
                        clientId = clientData.ContainsKey("UserId") ? clientData["UserId"]?.ToString() ?? string.Empty : string.Empty;
                        isAvailable = string.IsNullOrEmpty(clientId);
                    }
                    Hour hour = new Hour(i) { ClientName = clientName, ClientId = clientId, IsAvailable = isAvailable };
                    dateModel.AddHour(hour);
                }
            }
        }
        #endregion
    }
}

