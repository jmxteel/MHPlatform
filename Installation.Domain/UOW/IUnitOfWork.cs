using Installation.Domain.IRepository;
using Installation.Domain.SQLBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.UOW
{
    public interface IUnitOfWork
    {
        IGeneric<T> GetRepository<T>() where T : class;
        Task SaveChangesAsync();

    }
}
