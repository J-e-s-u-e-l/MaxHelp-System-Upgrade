using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class SalesReportController : Controller
    {
        private readonly DataDbContext _dataDbContext;

        public SalesReportController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public IActionResult Index(string selectedSalesPeriod, string selectedRevenuePeriod)
        {
            var businessUnitId = int.Parse(User.Claims.First(x => x.Type == "BusinessUnitId").Value);

            var test = _dataDbContext.Sales
                .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate == DateTime.Today)
                .Count();

            // Fetch data for the sales report page
            var viewModel = new SalesReportViewModel
            {
                TotalUnitsSoldToday = _dataDbContext.Sales
                    .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate >= DateTime.Today && s.SaleDate < DateTime.Today.AddDays(1))
                    .Count(),

                TotalUnitsSoldLastWeek = _dataDbContext.Sales
                    .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate >= DateTime.Today.AddDays(-7) && s.SaleDate < DateTime.Today)
                    .Count(),

                TotalUnitsSoldLastMonth = _dataDbContext.Sales
                    .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate >= DateTime.Today.AddDays(-31) && s.SaleDate < DateTime.Today)
                    .Count(),

                TotalRevenueToday = _dataDbContext.Sales
                    .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate >= DateTime.Today && s.SaleDate < DateTime.Today.AddDays(1))
                    .Sum(s => (decimal?)s.Amount) ?? 0,

                TotalRevenueLastWeek = _dataDbContext.Sales
                    .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate >= DateTime.Today.AddDays(-7) && s.SaleDate < DateTime.Today)
                    .Sum(s => (decimal?)s.Amount) ?? 0,

                TotalRevenueLastMonth = _dataDbContext.Sales
                    .Where(s => s.BusinessUnitId == businessUnitId && s.SaleDate >= DateTime.Today.AddDays(-31) && s.SaleDate < DateTime.Today)
                    .Sum(s => (decimal?)s.Amount) ?? 0,

                TopSellingProducts = _dataDbContext.Sales
                    .Where(s => s.BusinessUnitId == businessUnitId)
                    .GroupBy(s => s.ProductName)
                    .OrderByDescending(g => g.Sum(x => x.Amount))
                    .Take(3)
                    .Select(g => new TopSalesItem
                    {
                        ItemName = g.Key,
                        SalesCount = g.Count()
                    })
                    .ToList(),

                SelectedSalesPeriod = selectedSalesPeriod,
                SelectedRevenuePeriod = selectedRevenuePeriod
            };

            return View(viewModel);
        }
    }
}
