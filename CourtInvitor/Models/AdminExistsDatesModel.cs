using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourtInvitor.Models
{
    public class AdminExistsDatesModel
    {
        //public abstract string Name { get;set; }
        //public abstract string[] arr { get; set; }
        //public abstract int temp { get; set; }


        public string DateText { get; set; } = string.Empty;
        public ICommand ClickCommand { get; set; }


    }
}
