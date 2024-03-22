using Installation.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.IService
{
    public interface IGenericService<TDto, T>where TDto : class where T : class
    {
        Task<IEnumerable<TDto?>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int? id);
        Task AddAsync(TDto entity);
        Task UpdateAsync(TDto entity);
        Task DeleteAsync(TDto entity);
    }
}
