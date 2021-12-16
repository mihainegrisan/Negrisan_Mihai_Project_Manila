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
    public class SeedOrder
    {
        public static void Seed(IServiceProvider serviceProvider, int count, int numberOfCustomersSeeded)
        {
            var context = serviceProvider.GetRequiredService<ProjectManilaDBContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Order', RESEED, 0)");

            if (context.Orders.Count() >= count)
            {
                return;
            }
            

            for (var i = 0; i < count; ++i)
            {
                var id = i % numberOfCustomersSeeded + 1;
                var customer = context.Customers.FirstOrDefault(c => c.CustomerId == id);

                if (customer?.Address is null)
                {
                    return;
                }

                var order = new Faker<Order>()
                    .RuleFor(o => o.OrderDate, f => f.Date.Between(customer.EntryDate, DateTime.Now))
                    .RuleFor(o => o.Customer, f => customer)
                    .RuleFor(o => o.ShippingAddress, f => customer.Address);

                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
    }
}
