using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Manila.DAL;
using Project_Manila.DAL.Seed;

namespace Project_Manila.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ProjectManilaDBContext>();
                    context.Database.Migrate();

                    if (context.Addresses.Count() < 20)
                    {
                        SeedAddress.Seed(services, 20);
                    }

                    if (context.Customers.Count() < 20)
                    {
                        SeedCustomer.Seed(services, 20);
                    }

                    if (context.Products.Count() < 20)
                    {
                        SeedProduct.Seed(services, 20);
                    }

                    if (context.Orders.Count() < 20)
                    {
                        SeedOrder.Seed(services, 20);
                    }

                    if (context.OrderItems.Count() < 20)
                    {
                        SeedOrderItem.Seed(services, 20);
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
