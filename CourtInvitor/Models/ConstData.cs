namespace CourtInvitor.Models
{
    /// <summary>
    /// Contains constant data values used throughout the application.
    /// </summary>
    internal static class ConstData
    {
        /// <summary>
        /// Minimum characters required in username.
        /// </summary>
        public const int MinCharacterInUN = 3;
        /// <summary>
        /// Minimum characters required in password.
        /// </summary>
        public const int MinCharacterInPW = 8;
        /// <summary>
        /// Minimum characters required in email.
        /// </summary>
        public const int MinCharacterInEmail = 5;
        /// <summary>
        /// Collection name for clubs in Firebase.
        /// </summary>
        public const string Clubs = "clubs";
        /// <summary>
        /// Number of days in a week.
        /// </summary>
        public const int DaysInWeek = 7;
        /// <summary>
        /// Number of bookable hours per day.
        /// </summary>
        public const int HoursPerDay = 17;
        /// <summary>
        /// Date format for court day keys.
        /// </summary>
        public const string DateFormat = "dd.MM.yyyy";
    }
}
