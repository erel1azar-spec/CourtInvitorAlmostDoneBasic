using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    public abstract class SessionModel
    {
        public abstract string TimeLeft { get; protected set; }
        public abstract void RegisterTimer();
    }
}
