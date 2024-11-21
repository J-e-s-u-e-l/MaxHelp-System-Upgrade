using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DataDbContext _dataDbContext;

        public DashboardController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }
        public IActionResult Index(string selectedSalesPeriod)
        {
            var businessUnitId = int.Parse(User.Claims.First(x => x.Type == "BusinessUnitId").Value);

            // Fetch Data for the dashboard
            var viewModel = new DashboardViewModel
            {
                TotalSalesToday = _dataDbContext.Sales
                .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate.Date == DateTime.Today)
                .Sum(s => (decimal?)s.Amount) ?? 0,

                TotalSalesYesterday = _dataDbContext.Sales
                .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate.Date == DateTime.Today.AddDays(-1))
                .Sum(s => (decimal?)s.Amount) ?? 0,

                TopSales = _dataDbContext.Sales
                .Where(s => s.BusinessUnitId == businessUnitId)
                .GroupBy(s => s.ProductName)
                .OrderByDescending(g => g.Sum(x => x.Amount))
                .Take(3)
                .Select(g => new TopSalesItem
                {
                    ItemName = g.Key,
                    SalesCount = g.Count(),
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .ToList(),

                TotalUnitSoldThisWeek = _dataDbContext.Sales
                .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate >= DateTime.Today.AddDays(-7))
                .Count(),

                TotalRevenue = _dataDbContext.Sales
                .Where(s => s.BusinessUnitId == businessUnitId)
                .Sum(s => (decimal?)s.Amount) ?? 0,

                LowStockItemsCount = _dataDbContext.Inventories
                .Where(i => i.BusinessUnitId == businessUnitId && i.ProductQuantity < i.ReorderThreshold)
                .Count(),

                SelectedSalesPeriod = selectedSalesPeriod
            };

            return View(viewModel);
        }
    }
}
