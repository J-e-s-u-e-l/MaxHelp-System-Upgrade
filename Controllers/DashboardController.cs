using MaxHelp_System_Upgrade.Data;
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
        public IActionResult Index()
        {
            var businessUnitId = int.Parse(User.Claims.First(x => x.Type == "BusinessUnitId").Value);

            var inventoryCount = _dataDbContext.InventoryItems
                .Where(i => i.BusinessUnitId == businessUnitId)
                .Count();

            var financialReport = _dataDbContext.FinancialReports
                .Where(x => x.BusinessUnitId == businessUnitId && x.Date == DateTime.Today)
                .ToList();

            //var viewModel = new DashBoardViewModel
            //{

            //};

            return View();
        }
    }
}
