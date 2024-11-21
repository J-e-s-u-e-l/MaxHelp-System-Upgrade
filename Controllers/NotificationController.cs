using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class NotificationController : Controller
    {
        private readonly DataDbContext _dataDbContext;

        public NotificationController(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public IActionResult GetNotifications()
        {
            var businessUnitId = int.Parse(User.Claims.First(x => x.Type == "BusinessUnitId").Value);

            var notifications = _dataDbContext.InventoryItems
                .Where(item => item.BusinessUnitId == businessUnitId && item.ProductQuantity <= item.ReorderThreshold)
                .Include(item => item.BusinessUnit)
                .Select(item => new
                {
                    BusinessUnitName = item.BusinessUnit.Name,
                    ItemName = item.ProductName
                })
                .ToList();

            //return Json(new {data = notifications});
            return Json(notifications);
        }
    }
}
