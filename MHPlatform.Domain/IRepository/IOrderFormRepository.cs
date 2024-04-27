using Installation.Domain.IRepository;
using MHPlatform.Domain.Entities;
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
    }
}
