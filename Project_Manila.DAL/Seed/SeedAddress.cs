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
    public class SeedAddress
    {
        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ProjectManilaDBContext>();
            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT('Address', RESEED, 0)");

            if (context.Addresses.Count() >= count)
            {
                return;
            }

            for (var i = 0; i < count; ++i)
            {
                var address = new Faker<Address>()
                    .RuleFor(a => a.StreetAddress, f => f.Address.StreetAddress())
                    .RuleFor(a => a.StreetName, f => f.Address.StreetName())
                    .RuleFor(a => a.City, f => f.Address.City())
                    .RuleFor(a => a.State, f => f.Address.State())
                    .RuleFor(a => a.Country, f => f.Address.Country())
                    .RuleFor(a => a.PostalCode, f => f.Address.ZipCode());

                context.Addresses.Add(address);
                context.SaveChanges();
            }
        }
    }
}
