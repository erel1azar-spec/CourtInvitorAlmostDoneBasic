namespace CourtInvitor.Models
{
    internal abstract class CreateClubModel
    {

        public abstract string ClubName { get; set; }
        public abstract string Location { get; set; }
        public abstract string Phone { get; set; }
        public abstract string Email { get; set; }
        public abstract int CourtsCount { get; set; }
        public abstract string StatusMessage { get; }

        public abstract Task CreateClubAsync(DateTime startDate);
        //public abstract string ClubName { get; set; }
        //public abstract string Location { get; set; }
        //public abstract string Phone { get; set; }
        //public abstract string Email { get; set; }
        //public abstract int CourtsCount { get; set; }
        //public abstract string StatusMessage { get; set; }
        //public abstract Task<bool> CreateClubAsync(DateTime startDate, Action<System.Threading.Tasks.Task> onComplete);
    }
}

