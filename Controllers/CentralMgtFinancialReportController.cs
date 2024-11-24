using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.Models;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class CentralMgtFinancialReportController : Controller
    {
        private readonly DataDbContext _dataDbContext;

        public CentralMgtFinancialReportController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public IActionResult Index(string selectedRevenuePeriod, string selectedTopSalesPeriod)
        //public IActionResult Index(string selectedSalesPeriod, string selectedRevenuePeriod, string selectedTopSalesPeriod)
        {
            IQueryable<Sales> selectedSalesQuery;

            switch (selectedRevenuePeriod)
            {
                case "Today":
                    selectedSalesQuery = _dataDbContext.Sales.Where(s => s.SaleDate.Date == DateTime.Today);
                    break;

                case "This Week":
                    selectedSalesQuery = _dataDbContext.Sales.Where(s => s.SaleDate >= DateTime.Today.AddDays(-7));
                    break;

                case "Last Month":
                    selectedSalesQuery = _dataDbContext.Sales.Where(s => s.SaleDate >= DateTime.Today.AddMonths(-1));
                    break;

                default:
                    selectedSalesQuery = _dataDbContext.Sales.Where(s => s.SaleDate.Date == DateTime.Today);
                    break;
            }


            // Fetch Data for the Financial Report page
            var viewModel = new CentralMgtFinancialReportViewModel
            {
                /*TotalUnitSoldToday = _dataDbContext.Sales
                    .Where(s => s.SaleDate.Date == DateTime.Today)
                    .Count(),

                TotalUnitSoldThisWeek = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddDays(-7))
                    .Count(),

                TotalUnitSoldLastMonth = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddMonths(-1))
                    .Count(),
*/
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
                    .Select(g => new RevenueItem
                    {
                        // Join Sales with BusinessUnits to get the name
                        BusinessUnitName = _dataDbContext.BusinessUnits.FirstOrDefault(bu => bu.Id == g.Key).Name ?? "Unknown BU", // Handle missing business unit
                        Revenue = g.Sum(x => x.Amount)
                    })
                    .ToList(),

                TotalRevenueThisWeek = _dataDbContext.Sales
                  .Where(s => s.SaleDate >= DateTime.Today.AddDays(-7))
                  .GroupBy(s => s.BusinessUnitId)
                  .Select(g => new RevenueItem
                  {
                      // Join Sales with BusinessUnits to get the name
                      BusinessUnitName = _dataDbContext.BusinessUnits.FirstOrDefault(bu => bu.Id == g.Key).Name ?? "Unknown BU", // Handle missing business unit
                      Revenue = g.Sum(x => x.Amount)
                  })
                  .ToList(),

                TotalRevenueLastMonth = _dataDbContext.Sales
                    .Where(s => s.SaleDate >= DateTime.Today.AddMonths(-1))
                    .GroupBy(s => s.BusinessUnitId)
                    .Select(g => new RevenueItem
                    {
                        // Join Sales with BusinessUnits to get the name
                        BusinessUnitName = _dataDbContext.BusinessUnits.FirstOrDefault(bu => bu.Id == g.Key).Name ?? "Unknown BU", // Handle missing business unit
                        Revenue = g.Sum(x => x.Amount)
                    })
                    .ToList(),
/*
                LowStockItems = _dataDbContext.Inventories
                    .Where(i => i.ProductQuantity < i.ReorderThreshold)
                    .Select(i => new LowStockItem
                    {
                        BusinessUnit = i.BusinessUnit.Name,
                        ItemName = i.ProductName,
                        StockLeft = i.ProductQuantity
                    })
                    .ToList(),*/

                // Calculating total revenue for all business units
                TotalRevenueForAllBU = selectedSalesQuery.Sum(s => s.Amount),

                SelectedRevenuePeriod = selectedRevenuePeriod,
                //SelectedSalesPeriod = selectedSalesPeriod,
                SelectedTopSalesPeriod = selectedTopSalesPeriod
            };

            return View(viewModel);
        }


    }
}
