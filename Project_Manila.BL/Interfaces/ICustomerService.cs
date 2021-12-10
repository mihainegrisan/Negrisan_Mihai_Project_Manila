using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Project_Manila.DAL.Models;

namespace Project_Manila.BL.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int? id);
        Task<bool> Add(Customer customer);
        Task<bool> Delete(int id);
        Task<bool> Update(Customer customer);
    }
}
