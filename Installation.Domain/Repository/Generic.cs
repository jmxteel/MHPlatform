using Installation.Domain.Context;
using Installation.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.Repository
{
    public class Generic<T>: IGeneric<T> where T : class
    {
        private readonly InstallationContext _context;

        public Generic(InstallationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result; 
        }

        public Task AddAsync(T entity)
        {
            _context.Set<T>().AddAsync(entity);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChangesAsync();
        }

    }
}
