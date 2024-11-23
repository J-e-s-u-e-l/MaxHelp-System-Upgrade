using MaxHelp_System_Upgrade.Data;
using MaxHelp_System_Upgrade.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MaxHelp_System_Upgrade
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MaxHelpDB"));
            });

            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DataDbContext>()
                .AddDefaultTokenProviders();

            /*builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "";
                options.AccessDeniedPath = "";
            }); */

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await SeedData.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseMiddleware<BusinessUnitMiddleware>();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            /*app.MapControllerRoute(
                name: "centralManagementDashboard",
                pattern: "CentralManagement/Dashboard/{action=Index}/{id?}",
                defaults: new { controller = "Dashboard" });*/

            app.Run();
        }
    }
}
