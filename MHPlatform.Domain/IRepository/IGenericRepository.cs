using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetAllAsync();

        /// <summary>
        /// Find specific data in the entity using id value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Find specific record in identity depending on predicate value declared on controller level
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<T?> FindByConditionAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds New Data in Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T? entity);

        /// <summary>
        /// Update Entity Records
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(T? entity);

        /// <summary>
        /// Delete Entity Record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(T? entity);

        /// <summary>
        /// Save Entity Changes
        /// </summary>
        /// <returns></returns>
        Task<bool> SaveChangesAsync();

    }
}
