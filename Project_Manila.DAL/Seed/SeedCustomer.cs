using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Project_Manila.DAL.Models;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;

namespace Project_Manila.DAL.Seed
{
    public class SeedCustomer
    {
        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ProjectManilaDBContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Customer', RESEED, 0)");

            if (context.Customers.Count() >= count)
            {
                return;
            }

            for (var i = 1; i <= count; ++i)
            {
                var address = context.Addresses.FirstOrDefault(a => a.AddressId == i);

                if (address is null)
                {
                    return;
                }

                var firstName = "";
                var lastName = "";

                var customer = new Faker<Customer>()
                    .RuleFor(c => c.FirstName, f => firstName = f.Person.FirstName)
                    .RuleFor(c => c.LastName, f => lastName = f.Person.LastName)
                    .RuleFor(c => c.Age, f => f.Random.Number(12, 100))
                    .RuleFor(c => c.EmailAddress, f => f.Internet.Email(firstName, lastName))
                    .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber("##########"))
                    .RuleFor(c => c.EntryDate, f => f.Date.Between(DateTime.Parse("2017-01-01"), DateTime.Now))
                    .RuleFor(c => c.Address, f => address);

                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }
    }
}
