using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtInvitor.Models
{
    public abstract class ClientExistingClubListModel
    {
        //public string ClubText { get; set; } = string.Empty;
        //public ICommand ClickCommand { get; set; }
        public abstract string ClubText { get; }
    }
}
