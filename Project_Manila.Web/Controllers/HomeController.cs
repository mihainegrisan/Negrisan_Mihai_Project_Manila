using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Project_Manila.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Project_Manila.DAL;
using Project_Manila.DAL.ViewModels;

namespace Project_Manila.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjectManilaDBContext _db;

        public HomeController(ILogger<HomeController> logger, ProjectManilaDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderAndItemsGroup> data = _db.Orders
                .Include(o => o.OrderItems)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderAndItemsGroup()
                {
                    OrderDate = o.OrderDate,
                    ItemsPerOrder = o.OrderItems.Sum(oi => oi.Quantity),
                    TotalAmountOrdered = o.OrderItems.Sum(oi => oi.PurchasePrice * oi.Quantity)
                });

            return View(await data.AsNoTracking().ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
