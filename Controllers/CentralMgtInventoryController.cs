using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.Models;
using MaxHelp_System_Upgrade.Services.Interfaces;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class CentralMgtInventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly DataDbContext _dataDbContext;

        public CentralMgtInventoryController(IInventoryService inventoryService, DataDbContext dataDbContext)
        {
            _inventoryService = inventoryService;
            _dataDbContext = dataDbContext;
        }

        public IActionResult Index(string searchQuery, string filter = "All")
        {
            int? businessUnitId = filter switch
            {
                "Groceries" => _dataDbContext.BusinessUnits.FirstOrDefault(x => x.Name == filter).Id,
                "Bookshop" => _dataDbContext.BusinessUnits.FirstOrDefault(x => x.Name == filter).Id,
                "BottledWater" => _dataDbContext.BusinessUnits.FirstOrDefault(x => x.Name == filter).Id,
                "Restaurant" => _dataDbContext.BusinessUnits.FirstOrDefault(x => x.Name == filter).Id,
                _ => null, // "All"
            };

            var inventory = _inventoryService.GetInventory(businessUnitId, searchQuery);
            ViewBag.Filter = filter;
            ViewBag.SearchQuery = searchQuery;

            return View(inventory);
        }

        public IActionResult AddProduct()
        {
            return View("~/Views/Shared/AddProduct.cshtml", new InventoryViewModel());
        }

        [HttpPost]
        public IActionResult AddProduct(InventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.BusinessUnitId = int.Parse(User.Claims.First(i => i.Type == "BusinessUnitId").Value);

                var inventory = new Inventory
                {
                    ProductName = model.ProductName,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    BusinessUnitId = model.BusinessUnitId
                };

                _inventoryService.AddProduct(inventory);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult EditProduct(int id)
        {
            var product = _inventoryService.GetInventoryById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(CentralMgtInventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var inventory = new Inventory
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice
                };

                _inventoryService.UpdateProduct(inventory);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult DeleteProduct(int id)
        {
            var product = _inventoryService.GetInventoryById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("ConfirmDeleteProduct")]
        public IActionResult ConfirmDeleteProduct(int id)
        {
            _inventoryService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}

