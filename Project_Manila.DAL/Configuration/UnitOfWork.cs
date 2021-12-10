using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_Manila.DAL.Interfaces;
using Project_Manila.DAL.Repositories;

namespace Project_Manila.DAL.Configuration
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ProjectManilaDBContext _context;
        private readonly ILogger _logger;
        public ICustomerRepository CustomerRepository { get; private set; }

        public UnitOfWork(ProjectManilaDBContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");

            CustomerRepository = new CustomerRepository(context, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
