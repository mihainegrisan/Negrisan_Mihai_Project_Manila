using Bogus;
using Microsoft.Extensions.DependencyInjection;
using Project_Manila.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project_Manila.DAL.Seed
{
    public class SeedProduct
    {
        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ProjectManilaDBContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Product', RESEED, 0)");

            if (context.Products.Count() >= 20)
            {
                return;
            }

            for (var i = 0; i < count; ++i)
            {
                var product = new Faker<Product>()
                    .RuleFor(o => o.ProductName, f => f.Commerce.ProductName())
                    .RuleFor(o => o.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(o => o.CurrentPrice, f => f.Random.Decimal(5M, 1500M));

                context.Products.Add(product);
                context.SaveChanges();
            }
        }
    }
}
