using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_Manila.DAL.Interfaces;

namespace Project_Manila.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ProjectManilaDBContext _context;
        internal DbSet<T> _dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(ProjectManilaDBContext context, ILogger logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        public virtual Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public virtual async Task<T> GetById(int? id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
