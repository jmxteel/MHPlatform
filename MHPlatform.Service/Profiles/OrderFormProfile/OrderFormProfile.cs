using AutoMapper;
using MHPlatform.Domain.Entities;
using MHPlatform.Service.Model.OrderForm;
using MHPlatform.Service.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Profiles.OrderFormProfile
{
    public class OrderFormProfile: Profile
    {
        public OrderFormProfile()
        {
            CreateMap<OrderFormDto, OrderForm>().ReverseMap();
        }
    }
}
