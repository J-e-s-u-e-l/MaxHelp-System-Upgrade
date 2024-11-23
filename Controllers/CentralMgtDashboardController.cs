using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaxHelp_System_Upgrade.Controllers
{
/*    [Area("CentralManagement")]
    [Route("/CentralManagement/Dashboard/Index")]*/
    public class CentralMgtDashboardController : Controller
    {
        private readonly DataDbContext _dataDbContext;

        public CentralMgtDashboardController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public IActionResult Index(string selectedSalesPeriod, string selectedRevenuePeriod)
        {
            // Fetch Data for the dashboard
            var viewModel = new CentralMgtDashboardViewModel
            {
                TotalUnitSoldToday = _dataDbContext.Sales
                    .Where(s => s.SaleDate.Date == DateTime.Today)
                    .Count(),

                TotalUnitSoldThisWeek = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddDays(-7))
                    .Count(),

                TotalUnitSoldLastMonth = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddMonths(-1))
                    .Count(),

                TopSalesToday = _dataDbContext.Sales
                    .Where(s => s.SaleDate.Date == DateTime.Today)
                    .GroupBy(s => s.ProductName)
                    .OrderByDescending(g => g.Sum(x => x.Amount))
                    .Take(5)
                    .Select(g => new TopSalesItem
                    {
                        ItemName = g.Key,
                        SalesCount = g.Count(),
                        TotalAmount = g.Sum(x => x.Amount)
                    })
                    .ToList(),

                TopSalesThisWeek = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddDays(-7))
                    .GroupBy(s => s.ProductName)
                    .OrderByDescending(g => g.Sum(x => x.Amount))
                    .Take(5)
                    .Select(g => new TopSalesItem
                    {
                        ItemName = g.Key,
                        SalesCount = g.Count(),
                        TotalAmount = g.Sum(x => x.Amount)
                    })
                    .ToList(),

                TopSalesLastMonth = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddMonths(-1))
                    .GroupBy(s => s.ProductName)
                    .OrderByDescending(g => g.Sum(x => x.Amount))
                    .Take(5)
                    .Select(g => new TopSalesItem
                    {
                        ItemName = g.Key,
                        SalesCount = g.Count(),
                        TotalAmount = g.Sum(x => x.Amount)
                    })
                    .ToList(),

                TotalRevenueToday = _dataDbContext.Sales
                    .Where(s => s.SaleDate.Date == DateTime.Today)
                    .GroupBy(s => s.BusinessUnitId)
                    //.GroupBy(s => s.BusinessUnitId.Name  )
                    .Select(g => new RevenueItem
                    {
                        BusinessUnitId = g.Key,
                        Revenue = g.Sum(x => x.Amount)
                    })
                    .ToList(),

                TotalRevenueThisWeek = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddDays(-7))
                    .GroupBy(s => s.BusinessUnitId)
                    .Select(g => new RevenueItem
                    {
                        BusinessUnitId = g.Key,
                        Revenue = g.Sum(x => x.Amount)
                    })
                    .ToList(),

                TotalRevenueLastMonth = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddMonths(-1))
                    .GroupBy(s => s.BusinessUnitId)
                    .Select(g => new RevenueItem
                    {
                        BusinessUnitId = g.Key,
                        Revenue = g.Sum(x => x.Amount)
                    })
                    .ToList(),

                LowStockItems = _dataDbContext.Inventories
                    .Where(i => i.ProductQuantity < i.ReorderThreshold)
                    .Select(i => new LowStockItem
                    {
                        BusinessUnit = i.BusinessUnit.Name,
                        ItemName = i.ProductName,
                        StockLeft = i.ProductQuantity
                    })
                    .ToList(),

                SelectedSalesPeriod = selectedSalesPeriod,
                SelectedRevenuePeriod = selectedRevenuePeriod
            };

            return View(viewModel);
        }
    }
}
