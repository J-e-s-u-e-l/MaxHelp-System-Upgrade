using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.Models;
using MaxHelp_System_Upgrade.Services.Interfaces;
using MaxHelp_System_Upgrade.ViewModels;

namespace MaxHelp_System_Upgrade.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly DataDbContext _dataDbContext;

        public InventoryService(DataDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }

        public List<InventoryViewModel> GetInventory(int? businessUnitId, string searchQuery)
        {
            var query = _dataDbContext.Inventories.AsQueryable();

            if (businessUnitId.HasValue)
                query = query.Where(i => i.BusinessUnitId == businessUnitId.Value);

            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(i => i.ProductName.Contains(searchQuery));

            // Join with BusinessUnit table to get BusinessUnitName
            var result = query
                .Join(
                    _dataDbContext.BusinessUnits,
                    inventory => inventory.BusinessUnitId,
                    businessUnit => businessUnit.Id,
                    (inventory, businessUnit) => new InventoryViewModel
                    {
                        Id = inventory.Id,
                        ProductName = inventory.ProductName,
                        ProductQuantity = inventory.ProductQuantity,
                        ProductPrice = inventory.ProductPrice,
                        BusinessUnitId = inventory.BusinessUnitId,
                        BusinessUnitName = businessUnit.Name,
                        ProductNumber = inventory.ProductNumber
                    })
                .ToList();

            return result;
            //return query.ToList();
        }

        public InventoryViewModel GetInventoryItemById(int id)
        {
            var inventoryItem = _dataDbContext.Inventories
                .Where(i => i.Id == id)
                .Select(i => new InventoryViewModel
                {
                    Id = i.Id,
                    ProductName = i.ProductName,
                    ProductNumber = i.ProductNumber,
                    ProductPrice = i.ProductPrice,
                    ProductQuantity = i.ProductQuantity
                }).FirstOrDefault();
            
            return inventoryItem;
        }
        /*public Inventory GetInventoryItemById(int id)
        {
            return _dataDbContext.Inventories.FirstOrDefault(i => i.Id == id);
        }*/

        public void AddProduct(Inventory inventory)
        {
            _dataDbContext.Inventories.Add(inventory);
            _dataDbContext.SaveChanges();
        }

        public void UpdateProduct(Inventory inventory)
        {
            var existingProduct = _dataDbContext.Inventories.FirstOrDefault(i => i.Id == inventory.Id);
            if (existingProduct != null)
            {
                existingProduct.ProductName = inventory.ProductName;
                existingProduct.ProductQuantity = inventory.ProductQuantity;
                existingProduct.ProductPrice = inventory.ProductPrice;
                _dataDbContext.SaveChanges();
            }
        }

        public void DeleteProduct(int id)
        {
            var product = _dataDbContext.Inventories.FirstOrDefault(i => i.Id == id);
            if (product != null)
            {
                _dataDbContext.Inventories.Remove(product);
                _dataDbContext.SaveChanges();
            }
        }
    }

}
