namespace MaxHelp_System_Upgrade.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalSalesToday { get; set; }
        public decimal TotalSalesYesterday { get; set; }
        public List<TopSalesItem> TopSales { get; set; }
        public int TotalUnitSoldThisWeek { get; set; }
        public decimal TotalRevenue { get; set; }
        public int LowStockItemsCount { get; set; }
    }

    public class TopSalesItem
    {
        public string ItemName { get; set; }
        public int SalesCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
