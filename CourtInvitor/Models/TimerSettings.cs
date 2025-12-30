namespace CourtInvitor.Models
{
    /// <summary>
    /// Settings for the session timer.
    /// </summary>
    /// <param name="totalTimeInMilliseconds">Total time in milliseconds.</param>
    /// <param name="intervalInMilliseconds">Interval in milliseconds.</param>
    public class TimerSettings(long totalTimeInMilliseconds, long intervalInMilliseconds)
    {
        /// <summary>
        /// Gets or sets the total time in milliseconds.
        /// </summary>
        public long TotalTimeInMilliseconds { get; set; } = totalTimeInMilliseconds;
        /// <summary>
        /// Gets or sets the interval in milliseconds.
        /// </summary>
        public long IntervalInMilliseconds { get; set; } = intervalInMilliseconds;
    }
}
