using MaxHelp_System_Upgrade.Models;
using MaxHelp_System_Upgrade.ViewModels;

namespace MaxHelp_System_Upgrade.Services.Interfaces
{
    public interface IInventoryService
    {
        /// <summary>
        /// Retrieves the inventory based on the business unit ID and search query.
        /// </summary>
        /// <param name="businessUnitId">Optional business unit ID for filtering.</param>
        /// <param name="searchQuery">Optional search query for filtering.</param>
        /// <returns>A list of inventory items.</returns>
        List<InventoryViewModel> GetInventory(int? businessUnitId, string searchQuery);

        /// <summary>
        /// Retrieves an inventory item by its ID.
        /// </summary>
        /// <param name="id">The ID of the inventory item.</param>
        /// <returns>The inventory item, or null if not found.</returns>
        Inventory GetInventoryById(int id);

        /// <summary>
        /// Adds a new product to the inventory.
        /// </summary>
        /// <param name="inventory">The inventory item to add.</param>
        void AddProduct(Inventory inventory);

        /// <summary>
        /// Updates an existing product in the inventory.
        /// </summary>
        /// <param name="inventory">The updated inventory item.</param>
        void UpdateProduct(Inventory inventory);

        /// <summary>
        /// Deletes a product from the inventory by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        void DeleteProduct(int id);
    }
}
