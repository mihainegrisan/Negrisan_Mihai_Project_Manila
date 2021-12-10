using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_Manila.DAL.Interfaces;
using Project_Manila.DAL.Models;

namespace Project_Manila.DAL.Repositories
{
public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
        public CustomerRepository(ProjectManilaDBContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Customer>> GetAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CustomerRepository));
                return new List<Customer>();
            }
        }

        public override async Task<bool> Add(Customer customer)
        {
            customer.EntryDate = DateTime.Now;
            await base.Add(customer);
            return true;
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var customer = await _dbSet.Where(c => c.CustomerId == id).FirstOrDefaultAsync();

                if (customer == null)
                {
                    return false;
                }

                _dbSet.Remove(customer);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CustomerRepository));
                return false;
            }
        }

        public override async Task<bool> Update(Customer entity)
        {
            try
            {
                var existingUser = await _dbSet.Where(c => c.CustomerId == entity.CustomerId).FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return await Add(entity);
                }

                existingUser.FirstName = entity.FirstName;
                existingUser.LastName = entity.LastName;
                existingUser.EmailAddress = entity.EmailAddress;
                existingUser.PhoneNumber = entity.PhoneNumber;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(CustomerRepository));
                return false;
            }
        }
}
}
