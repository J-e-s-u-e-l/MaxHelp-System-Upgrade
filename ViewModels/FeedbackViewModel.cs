namespace MaxHelp_System_Upgrade.ViewModels
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; }
        public string MessageSnippet { get; set; }
        public string DivisionOfComplaint { get; set; }
        public DateTime DateSentFormatted { get; set; }
        //public string DateSentFormatted { get; set; }
        public bool IsRead { get; set; }
        public string EmailIconColor { get; set; }
    }

}
