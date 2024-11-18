namespace MaxHelp_System_Upgrade.Models
{
    public class FinancialReport
    {
        public int Id { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
        public decimal Profit => Revenue - Expenses;
        public DateTimeOffset Date { get; set; }
        public int BusinessUnitId { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
    }
}
