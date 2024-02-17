using AutoMapper;
using Installation.Domain.Entities;
using Installation.Service.Model;
using Installation.Service.Model.Installation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.Profiles.InstallationProfile
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            CreateMap<FileFlow, FileFlowDto>().ReverseMap();
            CreateMap<FileFlowAreas, FileFlowAreasDto>().ReverseMap();
        }

    }
}
