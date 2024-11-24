namespace MaxHelp_System_Upgrade.ViewModels
{
    public class CentralMgtFinancialReportViewModel
    {
        // Total Units Sold
        /*public int TotalUnitSoldToday { get; set; }
        public int TotalUnitSoldThisWeek { get; set; }
        public int TotalUnitSoldLastMonth { get; set; }*/

        // Top Sales
        public List<TopSalesItem> TopSalesToday { get; set; }
        public List<TopSalesItem> TopSalesThisWeek { get; set; }
        public List<TopSalesItem> TopSalesLastMonth { get; set; }

        // Revenue
        public List<RevenueItem> TotalRevenueToday { get; set; }
        public List<RevenueItem> TotalRevenueThisWeek { get; set; }
        public List<RevenueItem> TotalRevenueLastMonth { get; set; }
        public decimal TotalRevenueForAllBU { get; set; }

        // Selected Periods
        public string SelectedSalesPeriod { get; set; }
        public string SelectedRevenuePeriod { get; set; }
        public string SelectedTopSalesPeriod { get; set; }
    }

    /*public class RevenueItem
    {
        public string BusinessUnitName { get; set; }
        public decimal Revenue { get; set; }
    }*/
}
