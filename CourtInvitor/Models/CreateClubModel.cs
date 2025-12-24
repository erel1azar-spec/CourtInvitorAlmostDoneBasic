using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    internal abstract class CreateClubModel
    {
        public abstract string ClubName { get; set; }
        public abstract string Location { get; set; }
        public abstract string Phone { get; set; }
        public abstract string Email { get; set; }
        public abstract int CourtsCount { get; set; }
        public abstract string StatusMessage { get; set; }
        public abstract Task<bool> CreateClubAsync(DateTime startDate, Action<System.Threading.Tasks.Task> onComplete);
    }
}

