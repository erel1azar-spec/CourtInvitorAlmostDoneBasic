using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    internal class CourtDay
    {
        public string Date { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public List<Client> Clients { get; set; } = new();
    }
}
