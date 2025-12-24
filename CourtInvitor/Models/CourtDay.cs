using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    internal class CourtDay
    {
        public string date { get; set; } = string.Empty;
        public int courtNumber { get; set; }
        public List<Client> clients { get; set; } = new();
    }
}
