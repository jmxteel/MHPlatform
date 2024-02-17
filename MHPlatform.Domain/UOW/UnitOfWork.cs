using AutoMapper;
using Installation.Domain.Context;
using Installation.Domain.IRepository;
using Installation.Domain.Repository;
using Installation.Domain.SQLBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.UOW
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly InstallationContext _context;

        public UnitOfWork(InstallationContext context)
        {
            _context = context;
        }

        public IGeneric<T> GetRepository<T>() where T : class
        {
            return new Generic<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
