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
            return await _context.Set<T>().Take(10).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result; 
        }

    }
}
