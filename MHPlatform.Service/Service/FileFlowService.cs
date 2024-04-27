using AutoMapper;
using Installation.Domain;
using Installation.Domain.Entities;
using Installation.Domain.IRepository;
using Installation.Domain.SQLBuilder;
using Installation.Domain.UOW;
using Installation.Service.IService;
using Installation.Service.Model.Installation;
using Installation.Service.ServiceHelper;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Domain.Repository;
using MHPlatform.Service.Model.OrderForm;
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
        private readonly IOrderFormRepository _orderFormRepository;

        public FileFlowService(IUnitOfWork unitOfWork, IMapper mapper, IFileFlowRepository fileFlowRepository, IOrderFormRepository orderFormRepository) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileFlowRepository = fileFlowRepository;
            _orderFormRepository = orderFormRepository;
        }
        
        public async Task<FileFlowDto?> GetFolderWithAreas(string ffSrc)
        {

            var result = await _fileFlowRepository.GetFolderWithAreas(ffSrc);
            var areas = await _fileFlowRepository.FileFlowAreasList(ffSrc);
            var client = await _orderFormRepository.GetClientDetailsAsync(result!.OrderID!);
            //Mapping
            var resultDto = _mapper.Map<FileFlowDto>(result);
            var clientDto = _mapper.Map<OrderFormDto>(client);
            var resultAreas = _mapper.Map<List<FileFlowAreasDto>>(areas);
            ResponseBuilder(resultDto, clientDto, resultAreas);

            return resultDto;
        }

        private static void ResponseBuilder(FileFlowDto resultObject, OrderFormDto client, List<FileFlowAreasDto> areas)
        {
            resultObject.Ordrno = client.Ordrno;
            resultObject.Ctitle = client.Ctitle;
            resultObject.CName = client.CName;
            resultObject.CSurname = client.CSurname;
            resultObject.CAdd = client.CAdd;
            resultObject.CCon = client.CCon;
            resultObject.Cmobile = client.Cmobile;
            resultObject.CFax = client.CFax;
            resultObject.ConPrsn = client.ConPrsn;
            resultObject.Areas = areas;
        }
    }
}