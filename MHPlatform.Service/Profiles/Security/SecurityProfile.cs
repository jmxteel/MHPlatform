using AutoMapper;
using Installation.Domain.Entities;
using Installation.Service.Model.Installation;
using MHPlatform.Domain.Entities;
using MHPlatform.Service.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Profiles.Security
{
    public class SecurityProfile: Profile
    {
        public SecurityProfile()
        {
            CreateMap<UserAuthBaseDto, UserAuthBase>().ReverseMap();
            CreateMap<UserClaimDto, UserClaim>().ReverseMap();
            CreateMap<UserBaseDto, UserBase>().ReverseMap();
        }
    }
}
