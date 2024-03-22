using AutoMapper;
using Installation.Domain;
using Installation.Domain.Entities;
using Installation.Domain.IRepository;
using Installation.Domain.SQLBuilder;
using Installation.Domain.UOW;
using Installation.Service.IService;
using Installation.Service.Model.Installation;
using Installation.Service.ServiceHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.Service
{
    public class FileFlowService: GenericService<FileFlowDto, FileFlow>, IFileFlowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileFlowRepository _fileFlowRepository;

        public FileFlowService(IUnitOfWork unitOfWork, IMapper mapper, IFileFlowRepository fileFlowRepository) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileFlowRepository = fileFlowRepository;
        }
        
        //public async Task<IEnumerable<FileFlowDto?>> GetFolderWithAreas(int id)
        public async Task<FileFlowDto?> GetFolderWithAreas(string ffSrc)
        {

            var result = await _fileFlowRepository.GetFolderWithAreas(ffSrc);
            //var resultDto = _mapper.Map<IEnumerable<FileFlowDto>>(result);
            var resultDto = _mapper.Map<FileFlowDto>(result);
            return resultDto;
        }
        
    }
}