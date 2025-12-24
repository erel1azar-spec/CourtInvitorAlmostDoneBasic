using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtInvitor.Models
{
    public abstract class LocationSuggestionModel
    {
        public List<string> Suggestions { get; set; } = new();

    }
}
