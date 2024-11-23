    using MaxHelp_System_Upgrade.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace MaxHelp_System_Upgrade.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var _dataDbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            
            // Seeding the Business Units Table
            if(!_dataDbContext.BusinessUnits.Any())
            {
                _dataDbContext.BusinessUnits.AddRange(
                    new BusinessUnit { Name = "Groceries", Location = "Mainland" },
                    new BusinessUnit { Name = "BookShop", Location = "Island" },
                    new BusinessUnit { Name = "Restaurant", Location = "Mainland" },
                    new BusinessUnit { Name = "Bottled water", Location = "Island" },
                    new BusinessUnit { Name = "CentralMgt", Location = "Island" }
                );
                await _dataDbContext.SaveChangesAsync();
            }

            // Seeding the Users Table
            if (!userManager.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();

                var centralMgtAdmin = new User
                {
                    UserName = "centralMgtAdmin",
                    Email = "centralmgtadmin@maxhelp.com",
                    NormalizedEmail = "CENTRALMGTADMIN@MAXHELP.COM",
                    BusinessUnitId = _dataDbContext.BusinessUnits.First(x => x.Name == "CentralMgt").Id,
                    TwoFactorEnabled = false
                };

                var groceriesAdmin = new User
                {
                    UserName = "GroceriesAdmin",
                    Email = "groceriesadmin@maxhelp.com",
                    NormalizedEmail = "GROCERIESADMIN@MAXHELP.COM",
                    BusinessUnitId = _dataDbContext.BusinessUnits.First(x => x.Name == "Groceries").Id,
                    TwoFactorEnabled = false
                };

                var bookShopAdmin = new User
                {
                    UserName = "BookShopAdmin",
                    Email = "bookshopadmin@maxhelp.com",
                    NormalizedEmail = "BOOKSHOPADMIN@MAXHELP.COM",
                    BusinessUnitId = _dataDbContext.BusinessUnits.First(x => x.Name == "BookShop").Id,
                    TwoFactorEnabled = false
                };

                var restaurantAdmin = new User
                {
                    UserName = "RestaurantAdmin",
                    Email = "restaurantadmin@maxhelp.com",
                    NormalizedEmail = "RESTAURANTADMIN@MAXHELP.COM",
                    BusinessUnitId = _dataDbContext.BusinessUnits.First(x => x.Name == "Restaurant").Id,
                    TwoFactorEnabled = false
                };

                var bottledWaterAdmin = new User
                {
                    UserName = "BottledWaterAdmin",
                    Email = "bottledWateradmin@maxhelp.com",
                    NormalizedEmail = "BOTTLEDWATERADMIN@MAXHELP.COM",
                    BusinessUnitId = _dataDbContext.BusinessUnits.First(x => x.Name == "Bottled Water").Id,
                    TwoFactorEnabled = false
                };

                centralMgtAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "centralmgt");
                groceriesAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "groceries");
                bookShopAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "bookshop");
                restaurantAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "restaurant");
                bottledWaterAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "bottledwater");

                _dataDbContext.Users.AddRange(centralMgtAdmin, groceriesAdmin, bookShopAdmin, restaurantAdmin, bottledWaterAdmin);

                _dataDbContext.SaveChanges();
            }

            // Seeding the Inventories Table
            if (!_dataDbContext.Inventories.Any())
            {
                #region InventoryData
                var groceriesProducts = new[]
                {
                    new { Name = "Rice", Price = 500 },
                    new { Name = "Beans", Price = 700 },
                    new { Name = "Bread", Price = 300 },
                    new { Name = "Milk", Price = 1200 },
                    new { Name = "Eggs", Price = 500 },
                    new { Name = "Chicken", Price = 1500 },
                    new { Name = "Apples", Price = 800 },
                    new { Name = "Bananas", Price = 400 },
                    new { Name = "Tomatoes", Price = 200 },
                    new { Name = "Fish", Price = 1000 }
                };

                var bookShopProducts = new[]
                {
                    new { Name = "Notebook", Price = 150 },
                    new { Name = "Pen", Price = 50 },
                    new { Name = "Dictionary", Price = 5000 },
                    new { Name = "Textbook", Price = 7000 },
                    new { Name = "Marker", Price = 200 },
                    new { Name = "Eraser", Price = 30 },
                    new { Name = "Ruler", Price = 100 },
                    new { Name = "Stapler", Price = 300 },
                    new { Name = "Calculator", Price = 3500 },
                    new { Name = "Files", Price = 250 }
                };

                var restaurantProducts = new[]
                {
                    new { Name = "Pizza", Price = 2500 },
                    new { Name = "Burger", Price = 2000 },
                    new { Name = "Fries", Price = 1000 },
                    new { Name = "Steak", Price = 5000 },
                    new { Name = "Salad", Price = 1500 },
                    new { Name = "Soda", Price = 300 },
                    new { Name = "Pasta", Price = 2200 },
                    new { Name = "Ice Cream", Price = 800 },
                    new { Name = "Soup", Price = 1200 },
                    new { Name = "Sandwich", Price = 1500 }
                };

                var bottledWaterProducts = new[]
                {
                    new { Name = "Small Bottle", Price = 50 },
                    new { Name = "Medium Bottle", Price = 100 },
                    new { Name = "Large Bottle", Price = 150 },
                    new { Name = "Pack (6 Small Bottles)", Price = 300 },
                    new { Name = "Pack (6 Medium Bottles)", Price = 600 },
                    new { Name = "Pack (6 Large Bottles)", Price = 900 },
                    new { Name = "5 Liter Bottle", Price = 500 },
                    new { Name = "10 Liter Bottle", Price = 1000 },
                    new { Name = "Pack (12 Small Bottles)", Price = 600 },
                    new { Name = "Pack (12 Medium Bottles)", Price = 1200 }
                };

                #endregion


                var random = new Random();

                var businessUnits = _dataDbContext.BusinessUnits.ToList();

                int unitsToProcess = 4;  // This ensures only 4 units (Groceries, BookShop, Restaurant and Bottled water) are seeded with inventory data

                foreach (var unit in businessUnits)
                {
                    if (unitsToProcess <= 0)
                        break;

                    var products = unit.Name switch
                    { 
                        "Groceries" => groceriesProducts,
                        "BookShop" => bookShopProducts,
                        "Restaurant" => restaurantProducts,
                        "Bottled water" => bottledWaterProducts,
                        _ => throw new Exception("Unkown business unit")
                    };

                    foreach (var product in products)
                    {
                        var inventory = new Inventory
                        {
                            ProductName = product.Name,
                            ProductQuantity = random.Next(10, 100), // Ensure some low-stock items
                            ProductPrice = product.Price.ToString(),
                            ReorderThreshold = 30,
                            BusinessUnitId = unit.Id,
                            ProductNumber = generateProductNumber()
                        };

                        _dataDbContext.Inventories.Add(inventory);
                    }

                    unitsToProcess--;       // Decrement the value of unitsToProcess in order to stop the inventory seeding at the 4th unit, excluding the CMS
                }
                _dataDbContext.SaveChanges();
            }

            // Seeding the sales table
            if (!_dataDbContext.Sales.Any())
            {
                var random = new Random();

                var inventories = _dataDbContext.Inventories.ToList();
                foreach (var inventory in inventories)
                {
                    for (int i = 0; i < random.Next(30, 50); i++) // Ensure at least 30 sales per product
                    {
                        var quantitySold = random.Next(1, 10); // Random quantity per transaction
                        var amount = quantitySold * int.Parse(inventory.ProductPrice); // Calculate total amount

                        var saleDate = DateTime.Now.AddDays(-random.Next(0, 31)); // Sales within the past month

                        var sale = new Sales
                        {
                            InventoryId = inventory.Id,
                            Quantity = quantitySold,
                            Amount = amount,
                            ProductName = inventory.ProductName,
                            SaleDate = saleDate,
                            BusinessUnitId = inventory.BusinessUnitId
                        };
                        _dataDbContext.Sales.Add(sale);
                    }
                }
                _dataDbContext.SaveChanges();

                var lowStockItems = _dataDbContext.Inventories
                    .Where(i => i.ProductQuantity <= i.ReorderThreshold)
                    .ToList();

                if (!lowStockItems.Any())
                {
                    var randomItem = _dataDbContext.Inventories.OrderBy(i => Guid.NewGuid()).First();
                    randomItem.ProductQuantity = 5; // Make it low stock
                    _dataDbContext.SaveChanges();
                }
            }

            // Seeding the Feedback table
            if (!_dataDbContext.Feedbacks.Any())
            {
                var random = new Random();
                var businessUnits = _dataDbContext.BusinessUnits.ToList();
                var divisions = _dataDbContext.BusinessUnits.Select(bu => bu.Name).ToArray();
            
                int unitsToProcess = 4;  // This ensures only 4 units (Groceries, BookShop, Restaurant and Bottled water) are seeded

                foreach (var unit in businessUnits)
                {
                    if (unitsToProcess <= 0)
                        break;

                    for (int i = 0; i < 20; i++)
                    {
                        var feedback = new Feedback
                        {
                            SenderEmail = $"user{i}@example.com",
                            Message = $"This is a sample feedback message number {i} for testing purposes.",
                            DivisionOfComplaint = unit.Name,
                            DateSent = DateTime.Now.AddDays(-random.Next(0, 60)),
                            IsRead = i % 2 == 0,
                            BusinessUnitId = unit.Id
                        };

                        _dataDbContext.Feedbacks.Add(feedback);
                    }
                    unitsToProcess--;       // Decrement the value of unitsToProcess in order to stop the seeding at the 4th unit, excluding the CMS
                }

                _dataDbContext.SaveChanges();
            }
        }

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
