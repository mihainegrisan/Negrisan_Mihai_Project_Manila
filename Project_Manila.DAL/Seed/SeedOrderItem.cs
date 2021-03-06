using Bogus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Manila.DAL.Models;

namespace Project_Manila.DAL.Seed
{
    public class SeedOrderItem
    {
        public static void Seed(IServiceProvider serviceProvider, int count, int ordersAndProductsNumber)
        {
            var context = serviceProvider.GetRequiredService<ProjectManilaDBContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('OrderItem', RESEED, 0)");

            if (context.OrderItems.Count() >= count)
            {
                return;
            }

            var randomGenerator = new Random();

            for (var i = 0; i < count; ++i)
            {
                var id = i % ordersAndProductsNumber + 1;
                var randomProductId = randomGenerator.Next(1, ordersAndProductsNumber + 1);
                var order = context.Orders.FirstOrDefault(o => o.OrderId == id);
                var product = context.Products.FirstOrDefault(p => p.ProductId == randomProductId);

                if (order is null || product is null)
                {
                    return;
                }

                var orderItem = new Faker<OrderItem>()
                    .RuleFor(o => o.Quantity, f => f.Random.Number(1, 100))
                    .RuleFor(o => o.PurchasePrice, f => f.Random.Decimal(5M, 1500M))
                    .RuleFor(o => o.Order, f => order)
                    .RuleFor(o => o.Product, f => product);

                context.OrderItems.Add(orderItem);
                context.SaveChanges();
            }
        }
    }
}
