using MaxHelp_System_Upgrade.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaxHelp_System_Upgrade.Data
{
    public class DataDbContext : IdentityDbContext<User>
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) {}

        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<Product> InventoryItems { get; set; }
        public DbSet<FinancialReport> FinancialReports { get; set; }
    }
}
