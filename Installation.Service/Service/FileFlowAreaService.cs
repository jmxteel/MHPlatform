using AutoMapper;
using Installation.Domain.Entities;
using Installation.Domain.IRepository;
using Installation.Domain.UOW;
using Installation.Service.IService;
using Installation.Service.Model.Installation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.Service
{
    public class FileFlowAreaService: GenericService<FileFlowAreasDto, FileFlowAreas>,IFileFlowAreaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileFlowAreaRepository _fileFlowAreaRepository;

        public FileFlowAreaService(IUnitOfWork unitOfWork, IMapper mapper, IFileFlowAreaRepository fileFlowAreaRepository) 
            : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileFlowAreaRepository = fileFlowAreaRepository;
        }
    }
}
