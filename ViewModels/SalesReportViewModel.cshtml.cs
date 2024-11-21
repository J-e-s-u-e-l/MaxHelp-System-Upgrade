namespace MaxHelp_System_Upgrade.ViewModels
{
    public class SalesReportViewModel
    {
        public int TotalUnitsSoldToday { get; set; }
        public int TotalUnitsSoldLastWeek { get; set; }
        public int TotalUnitsSoldLastMonth { get; set; }
        public List<TopSalesItem> TopSellingProducts { get; set; }
        public decimal TotalRevenueToday { get; set; }
        public decimal TotalRevenueLastWeek { get; set; }
        public decimal TotalRevenueLastMonth { get; set; }
        public string SelectedSalesPeriod { get; set; }
        public string SelectedRevenuePeriod { get; set; }
    }
}
