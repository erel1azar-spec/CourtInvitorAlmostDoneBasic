using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtInvitor.Models
{
    public abstract class ClientExistingCourtsListModel
    {
        public abstract int CourtNumber { get; }
        public abstract string CourtText { get; }
    }
}
