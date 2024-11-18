namespace MaxHelp_System_Upgrade.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public Product Product { get; set; }
    }
}
