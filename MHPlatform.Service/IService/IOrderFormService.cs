using Installation.Service.IService;
using MHPlatform.Domain.Entities;
using MHPlatform.Service.Model.OrderForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.IService
{
    public interface IOrderFormService: IGenericService<OrderFormDto,OrderForm>
    {
        Task<OrderFormDto> GetClientDetails(string ordrNo);
    }
}
