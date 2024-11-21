namespace MaxHelp_System_Upgrade.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductNumber{ get; set; } = generateProductNumber();
        public int ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public int ReorderThreshold { get; set; } = 30;
        public int BusinessUnitId { get; set; }
        public virtual BusinessUnit BusinessUnit { get; set; }    // Navigation property

        private static string generateProductNumber()
        {
            var random = new Random();
            string accumulator = "";

            for (int i = 1; i <= 7; i++)
            {
                string holder;

                if (i == 1)
                    holder = random.Next(1, 9).ToString();

                holder = random.Next(0, 9).ToString();

                accumulator += holder;
            }

            return accumulator;
        }
    }
}
