using AutoMapper;
using Installation.Domain.Entities;
using Installation.Domain.SQLBuilder;
using Installation.Domain.UOW;
using Installation.Service.Model.Installation;
using Installation.Service.Service;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Service.IService;
using MHPlatform.Service.Model.OrderForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Service
{
    public class OrderFormService: GenericService<OrderFormDto, OrderForm>, IOrderFormService
    {
        private readonly IOrderFormRepository _orderFormRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public OrderFormService(IOrderFormRepository orderFormRepository, IMapper mapper, IUnitOfWork unitOfWork): base(unitOfWork, mapper)
        { 
            this._orderFormRepository = orderFormRepository;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork; 
        }

        public async Task<OrderFormDto> GetClientDetails(string ordrNo)
        {
            var result = await _orderFormRepository.GetClientDetailsAsync(ordrNo);
            var resultDto = _mapper.Map<OrderFormDto>(result);

            return resultDto;
        }

    }
}
