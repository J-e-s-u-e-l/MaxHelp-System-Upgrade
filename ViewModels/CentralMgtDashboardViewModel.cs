namespace MaxHelp_System_Upgrade.ViewModels
{
    public class CentralMgtDashboardViewModel
    {
        // Total Units Sold
        public int TotalUnitSoldToday { get; set; }
        public int TotalUnitSoldThisWeek { get; set; }
        public int TotalUnitSoldLastMonth { get; set; }

        // Top Sales
        public List<TopSalesItem> TopSalesToday { get; set; }
        public List<TopSalesItem> TopSalesThisWeek { get; set; }
        public List<TopSalesItem> TopSalesLastMonth { get; set; }

        // Revenue
        public List<RevenueItem> TotalRevenueToday { get; set; }
        public List<RevenueItem> TotalRevenueThisWeek { get; set; }
        public List<RevenueItem> TotalRevenueLastMonth { get; set; }

        // Low Stock
        public List<LowStockItem> LowStockItems { get; set; }

        // Selected Periods
        public string SelectedSalesPeriod { get; set; }
        public string SelectedRevenuePeriod { get; set; }
    }
/*
    public class TopSalesItem
    {
        public string ItemName { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalAmount { get; set; }
    }*/

    public class RevenueItem
    {
        public int BusinessUnitId { get; set; }
        public decimal Revenue { get; set; }
    }

    public class LowStockItem
    {
        public string BusinessUnit { get; set; }
        public string ItemName { get; set; }
        public int StockLeft { get; set; }
    }
}

