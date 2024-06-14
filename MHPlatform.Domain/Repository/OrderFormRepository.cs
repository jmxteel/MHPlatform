using Installation.Domain;
using Installation.Domain.Context;
using Installation.Domain.Repository;
using Installation.Domain.SQLBuilder;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Repository
{
    public class OrderFormRepository: Generic<OrderForm>, IOrderFormRepository
    {
        private readonly InstallationContext _context;
        public OrderFormRepository(InstallationContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<OrderForm?> GetClientDetailsAsync(string ordrNo)
        {
            var query = new QueryBuilder().SQLQueryBuilder<OrderForm?>(DataManipulationEnum.SELECT, ordrNo, "1");
            var result = await _context.OrderForms!.FromSqlRaw(query).FirstOrDefaultAsync();

            return result;
        }

        public async Task<(IEnumerable<OrderForm>, PaginationMetaData)> GetAllClientAsync(string? filter, string? q, int pageNumber, int pageSize)
        {

            var collection = _context.OrderForm! as IQueryable<OrderForm>;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.Trim();
                collection = collection.Where(o => o.Ordrno == filter);

            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                collection = collection.Where(c => c.CSurname!.Contains(q)
                || (c.CName != null && c.CName.Contains(q)));
            }

            var totalItemCount = await collection.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var clients = await collection.OrderByDescending(a => a.ID)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (clients, paginationMetaData);
        }
    }
}
