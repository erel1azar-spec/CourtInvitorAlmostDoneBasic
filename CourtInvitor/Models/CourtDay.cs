namespace CourtInvitor.Models
{
    internal class CourtDay
    {
        public string Date { get; set; } = string.Empty;
        public int CourtNumber { get; set; }
        public List<Client> Clients { get; set; } = new();
    }
}
