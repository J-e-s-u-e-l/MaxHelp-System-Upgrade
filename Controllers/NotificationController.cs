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
            var businessUnitName = User.Claims.First(x => x.Type == "BusinessUnitName").Value;

            IQueryable<Inventory> query;

            if (businessUnitName == "centralMgtAdmin")
            {
                // Central Management: Fetch notifications for all units
                query = _dataDbContext.InventoryItems
                    .Where(item => item.ProductQuantity <= item.ReorderThreshold)
                    .Include(item => item.BusinessUnit);
            }
            else
            {
                // Individual Business Unit: Fetch notifications for the logged-in unit
                query = _dataDbContext.InventoryItems
                    .Where(item => item.BusinessUnitId == businessUnitId && item.ProductQuantity <= item.ReorderThreshold)
                    .Include(item => item.BusinessUnit);
            }

            // Project to the required format
            var notifications = query
                .Select(item => new
                {
                    BusinessUnitName = item.BusinessUnit.Name,
                    ItemName = item.ProductName
                })
                .ToList();

            //return Json(new { data = notifications });
            return Json(notifications);
        }

    }
}
