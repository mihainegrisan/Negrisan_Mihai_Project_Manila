using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Manila.Common.Utility;
using Project_Manila.DAL;
using Project_Manila.DAL.Models;
using Project_Manila.DAL.ViewModels;

namespace Project_Manila.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ProjectManilaDBContext _context;

        public CustomersController(ProjectManilaDBContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["FirstNameSortParam"] = string.IsNullOrEmpty(sortOrder) ? "FirstName_desc" : "";
            ViewData["LastNameSortParam"] = sortOrder == "LastName_asc" ? "LastName_desc" : "LastName_asc";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var customers = _context.Customers.Select(c => c);

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "FirstName_desc":
                    customers = customers.OrderByDescending(b => b.FirstName);
                    break;
                case "LastName_asc":
                    customers = customers.OrderBy(b => b.LastName);
                    break;
                case "LastName_desc":
                    customers = customers.OrderByDescending(b => b.LastName);
                    break;
                default:
                    customers = customers.OrderBy(b => b.FirstName);
                    break;
            }

            const int pageSize = 5;

            return View(await PaginatedList<Customer>.CreateAsync(customers.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id, int? orderId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerIndexData();
            viewModel.Customer = await _context.Customers
                .Include(c => c.Address)
                .Include(c => c.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            ViewData["CustomerID"] = id.Value;
            viewModel.Orders = viewModel.Customer.Orders;
            
            if (orderId != null)
            {
                ViewData["OrderID"] = orderId.Value;
                viewModel.OrderItems = viewModel.Orders.Single(o => o.OrderId == orderId).OrderItems;
            }

            return View(viewModel);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Age,EmailAddress,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.EntryDate = DateTime.Now;
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Age,EmailAddress,PhoneNumber")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
