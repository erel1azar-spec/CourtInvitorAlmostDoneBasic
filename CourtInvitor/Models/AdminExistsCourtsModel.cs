namespace CourtInvitor.Models
{
    public abstract class AdminExistsCourtsModel
    {
        //public string DateText { get; set; } = string.Empty;
        //public ICommand ClickCommand { get; set; }
        public abstract int CourtNumber { get; }
        public abstract string CourtText { get;}

    }
}
