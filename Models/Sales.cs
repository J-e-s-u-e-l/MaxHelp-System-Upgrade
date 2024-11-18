namespace MaxHelp_System_Upgrade.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public DateTime SaleDate { get; set; }
        public int BusinessUnitId { get; set; }
        public Inventory InventoryItem { get; set; }
    }
}
