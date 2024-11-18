namespace MaxHelp_System_Upgrade.Models
{
    public class BusinessUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Inventory> InventoryItem { get; set; }
    }
}