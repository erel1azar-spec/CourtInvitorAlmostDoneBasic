using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtReserve.Models
{
    public abstract class ClubModel
    {
        protected string statusMessage = string.Empty;
        protected bool isSuccess = false;
        public abstract string ClubName { get; set; }
        public abstract string Location { get; set; }
        public abstract string Phone { get; set; }
        public abstract string Email { get; set; }
        public abstract int CourtsCount { get; set; }
        public string StatusMessage => statusMessage;
        public bool IsSuccess => isSuccess;
        public abstract Task CreateClubAsync(DateTime startDate);
        protected abstract Task<bool> ClubExistsAsync();
        protected abstract void SaveClubToFirestore();
        protected abstract void CreateCourtsForWeek(DateTime startDate);
        protected abstract void CreateCourtDay(int courtNumber, DateTime date);
        protected abstract string ValidateFields();
        protected abstract bool IsPhoneValid(string phone);
        protected abstract bool IsEmailValid(string Email);
    }
}    



