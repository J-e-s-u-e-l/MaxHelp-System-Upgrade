namespace MaxHelp_System_Upgrade.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public int ReorderThreshold { get; set; }
        public int BusinessUnitId { get; set; }
        public BusinessUnit BusinessUnit { get; set; }
    }
}
