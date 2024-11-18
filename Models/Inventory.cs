namespace MaxHelp_System_Upgrade.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public int ReorderThreshold { get; set; } = 30;
        public int BusinessUnitId { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
    }
}
