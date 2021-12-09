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
        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ProjectManilaDBContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('OrderItem', RESEED, 0)");

            if (context.OrderItems.Count() >= 20)
            {
                return;
            }

            for (var i = 1; i <= count; ++i)
            {
                var order = context.Orders.FirstOrDefault(o => o.OrderId == i);
                var product = context.Products.FirstOrDefault(p => p.ProductId == i);

                if (order is null || product is null)
                {
                    continue;
                }

                var orderItem = new Faker<OrderItem>()
                    .RuleFor(o => o.Quantity, f => f.Random.Number(1, 100))
                    .RuleFor(o => o.PurchasePrice, f => f.Random.Decimal(5M, 1500M))
                    .RuleFor(o => o.OrderId, f => order.OrderId)
                    .RuleFor(o => o.Order, f => order)
                    .RuleFor(o => o.ProductId, f => product.ProductId)
                    .RuleFor(o => o.Product, f => product);

                context.OrderItems.Add(orderItem);
                context.SaveChanges();
            }
        }
    }
}
