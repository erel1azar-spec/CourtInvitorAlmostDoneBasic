using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    public class TimerSettings(long totalTimeInMilliseconds, long intervalInMilliseconds)
    {
        public long TotalTimeInMilliseconds { get; set; } = totalTimeInMilliseconds;
        public long IntervalInMilliseconds { get; set; } = intervalInMilliseconds;
    }
}
