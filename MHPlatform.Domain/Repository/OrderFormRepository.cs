using Installation.Domain;
using Installation.Domain.Context;
using Installation.Domain.Repository;
using Installation.Domain.SQLBuilder;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
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
    }
}
