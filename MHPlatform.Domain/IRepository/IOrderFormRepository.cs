using Installation.Domain.IRepository;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.IRepository
{
    public interface IOrderFormRepository: IGeneric<OrderForm>
    {
        Task<OrderForm?> GetClientDetailsAsync(string ordrNo);
        Task<(IEnumerable<OrderForm>, PaginationMetaData)> GetAllClientAsync(string? filter, string? q, int pageNumber, int pageSize);
    }
}
