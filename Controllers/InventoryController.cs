using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.Models;
using MaxHelp_System_Upgrade.Services.Interfaces;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class InventoryController : Controller
    {
        private readonly DataDbContext _dataDbContext;
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService, DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public IActionResult Index(string searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;

            var businessUnitId = int.Parse(User.Claims.First(x => x.Type == "BusinessUnitId").Value);

            var inventory = _dataDbContext.Inventories
                .Where(i => i.BusinessUnitId == businessUnitId)
                .Where(i => string.IsNullOrEmpty(searchQuery) || i.ProductName.Contains(searchQuery))
                .ToList();

            return View(inventory);
        }

        // Add a new Product (GET)
        /*public IActionResult AddProduct()
        {
            var model = new Inventory();
            return View(model);
        }*/
        public IActionResult AddProduct()
        {
            return View("~/Views/Shared/AddProduct.cshtml", new InventoryViewModel());
        }


        // Add a new Product (POST)
        /*[HttpPost]
        public IActionResult AddProduct(InventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.BusinessUnitId = int.Parse(User.Claims.First(i => i.Type == "BusinessUnitId").Value);

                var productModel = new Inventory()
                {
                    ProductName = model.ProductName,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ReorderThreshold = 30,
                    BusinessUnitId = model.BusinessUnitId
                };

                _dataDbContext.Inventories .Add(productModel);
                _dataDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }*/
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

        // Edit product (GET)
        /*public IActionResult EditProduct(int id)
        {
            var product = _dataDbContext.Inventories.FirstOrDefault(i => i.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }*/
        public IActionResult EditProduct(int id)
        {
            var product = _inventoryService.GetInventoryById(id);
            if (product == null) return NotFound();
            return View(product);
        }


        // Edit Product (POST)
        /*[HttpPost]
        public IActionResult EditProduct(InventoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _dataDbContext.Inventories.FirstOrDefault(i => i.Id ==  model.Id);
                if (product == null) return NotFound();

                product.ProductName = model.ProductName;
                product.ProductQuantity = model.ProductQuantity;
                product.ProductPrice = model.ProductPrice;

                _dataDbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }*/
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

        // Delete product (GET)
        /*public IActionResult DeleteProduct(int id)
        {
            var product = _dataDbContext.Inventories.FirstOrDefault(i => i.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }*/
        public IActionResult DeleteProduct(int id)
        {
            var product = _inventoryService.GetInventoryById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // Delete product (POST)
        /*[HttpPost, ActionName("ConfirmDeleteProduct")]
        public IActionResult ConfirmDeleteProduct(int id)
        {
            var product = _dataDbContext.Inventories.FirstOrDefault(i => i.Id == id);
            if (product == null) return NotFound();

            _dataDbContext.InventoryItems.Remove(product);
            _dataDbContext.SaveChanges();
            return RedirectToAction("Index");
        }*/
        [HttpPost, ActionName("ConfirmDeleteProduct")]
        public IActionResult ConfirmDeleteProduct(int id)
        {
            _inventoryService.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
