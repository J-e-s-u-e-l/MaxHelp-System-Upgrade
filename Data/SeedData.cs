using MaxHelp_System_Upgrade.Models;
using Microsoft.AspNetCore.Identity;

namespace MaxHelp_System_Upgrade.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            
            if(!context.BusinessUnits.Any())
            {
                context.BusinessUnits.AddRange(
                    new BusinessUnit { Name = "Groceries", Location = "Mainland" },
                    new BusinessUnit { Name = "BookShop", Location = "Island" },
                    new BusinessUnit { Name = "Restaurant", Location = "Mainland" },
                    new BusinessUnit { Name = "Bottled water", Location = "Island" }
                );
                await context.SaveChangesAsync();
            }

            //if (!userManager.Users.Any())
            //{
            //    var admin = new User
            //    {
            //        UserName = "admin",
            //        Email = "admin@example.com",
            //        BusinessUnitId = context.BusinessUnits.First().Id
            //    };
            //    await userManager.CreateAsync(admin, "Admin@123");
            //}

            if (!userManager.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>();

                var groceriesAdmin = new User
                {
                    UserName = "GroceriesAdmin",
                    Email = "groceriesadmin@maxhelp.com",
                    BusinessUnitId = context.BusinessUnits.First(x => x.Name == "Groceries").Id
                };

                var bookShopAdmin = new User
                {
                    UserName = "BookShopAdmin",
                    Email = "bookshopadmin@maxhelp.com",
                    BusinessUnitId = context.BusinessUnits.First(x => x.Name == "BookShop").Id
                };

                var restaurantAdmin = new User
                {
                    UserName = "RestaurantAdmin",
                    Email = "restaurantadmin@maxhelp.com",
                    BusinessUnitId = context.BusinessUnits.First(x => x.Name == "Restaurant").Id
                };

                var bottledWaterAdmin = new User
                {
                    UserName = "BottledWaterAdmin",
                    Email = "bottledWateradmin@maxhelp.com",
                    BusinessUnitId = context.BusinessUnits.First(x => x.Name == "Bottled Water").Id
                };

                groceriesAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "groceries");
                bookShopAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "bookshop");
                restaurantAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "restaurant");
                bottledWaterAdmin.PasswordHash = passwordHasher.HashPassword(groceriesAdmin, "bottledwater");

                context.Users.AddRange(groceriesAdmin, bookShopAdmin, restaurantAdmin, bottledWaterAdmin);

                context.SaveChanges();

                //await userManager.CreateAsync(groceriesAdmin);
                //await userManager.CreateAsync(bookShopAdmin);
                //await userManager.CreateAsync(restaurantAdmin);
                //await userManager.CreateAsync(bottledWaterAdmin);
            }
        }
    }
}
