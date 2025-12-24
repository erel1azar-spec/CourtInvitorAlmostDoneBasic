using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtInvitor.Models
{
    internal class ClientExistingDatesListModel
    {
        public string DateText { get; set; } = string.Empty;
        public ICommand ClickCommand { get; set; }
    }
}
