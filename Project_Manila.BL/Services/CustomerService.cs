using Project_Manila.DAL.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Manila.BL.Interfaces;
using Project_Manila.DAL.Models;

namespace Project_Manila.BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Customer>> GetAll()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAll();
            return customers;
        }

        public async Task<Customer> GetById(int? id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(id);

            if (customer == null)
                return new Customer();

            return customer;
        }

        public Task<bool> Add(Customer customer)
        {
            //if (ModelState.IsValid)
            //{
            //    employee.Id = Guid.NewGuid();

            //    await _unitOfWork.Employee.Add(employee);
            //    await _unitOfWork.CompleteAsync();

            //    return CreatedAtAction("GetEmployee", new { employee.Id }, employee);
            //}

            //return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
