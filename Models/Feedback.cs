namespace MaxHelp_System_Upgrade.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; }
        public string Message { get; set; }
        public string DivisionOfComplaint { get; set; }
        public DateTime DateSent { get; set; }
        public bool IsRead { get; set; }
        public int BusinessUnitId { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
    }

}
