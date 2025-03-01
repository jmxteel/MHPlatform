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
using MHPlatform.Domain.Models;
using MHPlatform.Domain.Repository;
using MHPlatform.Service.IService;
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
        private readonly IOrderFormService _orderFormService;
        private readonly IFileFlowAreaService _fileFlowAreaService;

        public FileFlowService(IUnitOfWork unitOfWork, IMapper mapper, IFileFlowRepository fileFlowRepository, IOrderFormService orderFormService, IFileFlowAreaService fileFlowAreaService) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileFlowRepository = fileFlowRepository;
            _orderFormService = orderFormService;
            _fileFlowAreaService = fileFlowAreaService;
        }

        public async Task<(IEnumerable<FileFlowDto>, PaginationMetaData)> GetFileFlowPaginated(string? filter, string? q, int pageNumber, int pageSize)
        {
            var (result, pagination) = await _fileFlowRepository.GetFileFlowPaginated(filter, q, pageNumber, pageSize);
            var resultDto = _mapper.Map<IEnumerable<FileFlowDto>>(result);
            var customerDetails = await _orderFormService.FindByConditionAsync(x => result.Select(x => x.OrderID).Contains(x.Ordrno));
            var fileFlowAreas = await _fileFlowAreaService.FindByConditionAsync(o => result.Select(x => x.ID).Contains(o.ffID));


            var fileFlow = resultDto.Select(f => new FileFlowDto
            {
                ID = f.ID,
                OrderID = f.OrderID,
                FileFlowNo = f.FileFlowNo,
                Datecreated = f.Datecreated,
                OTfactor = f.OTfactor,
                OTchkbx = f.OTchkbx,
                Priority = f.Priority,
                ApprxDelDate = f.ApprxDelDate,
                ProjectList = f.ProjectList,
                ApprDelDateTxt = f.ApprDelDateTxt,
                PaymentReceived = f.PaymentReceived,
                FileMadeUp = f.FileMadeUp,
                DesignConsultant = f.DesignConsultant,
                TehcnicalRep = f.TehcnicalRep,
                FileIn = f.FileIn,
                CMDate = f.CMDate,
                TargetDate = f.TargetDate,
                FileOut = f.FileOut,
                OverTargetDate = f.OverTargetDate,
                Reasons = f.Reasons,
                LeadClosed = f.LeadStart,
                LeadStart = f.LeadStart,
                Revision = f.Revision,
                ChckddocsDate = f.ChckddocsDate,
                ChckdforTechnclDate = f.ChckdforTechnclDate,
                ApprovedDate = f.ApprovedDate,
                Recvinplant = f.Recvinplant,
                Shwrm = f.Shwrm,
                Deleted = f.Deleted,
                Deletedby = f.Deletedby,
                GrpngSysGen = f.GrpngSysGen,
                GrpngCtgry = f.GrpngCtgry,
                GrpngMat = f.GrpngMat,
                ManualGrpngCtgry = f.ManualGrpngCtgry,
                ManualGrpngMat = f.ManualGrpngMat,
                ChckbySalesDesigner = f.ChckbySalesDesigner,
                Variation = f.Variation,
                WorkingDaysOver = f.WorkingDaysOver,
                WorkingDaysUnder = f.WorkingDaysUnder,
                FFsrc = f.FFsrc,
                FGrouping = f.FGrouping,
                MGrouping = f.MGrouping,
                SysVer =f.SysVer,
                Typ = f.Typ,
                Cbfpaging = f.Cbfpaging,
                Fpaging = f.Fpaging,
                Islock = f.Islock,
                Lockedby = f.Lockedby,
                MHGrouping = f.MHGrouping,
                Samplecolor = f.Samplecolor,

                //OrderForm Details
                Ordrno = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.Ordrno ?? "",
                Ctitle = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.Ctitle ?? "",
                CName = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.CName ?? "",
                CSurname = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.CSurname ?? "",
                CAdd = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.CAdd ?? "",
                InsAdd = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.InsAdd ?? "",
                CCon = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.CCon ?? "",
                Cmobile = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.Cmobile ?? "",
                CFax = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.CFax ?? "",
                ConPrsn = customerDetails.FirstOrDefault(o => o.Ordrno == f.OrderID)?.ConPrsn ?? "",

                Areas = fileFlowAreas
                    .Where(area => area.ffID == f.ID)
                    .ToList() ?? new List<FileFlowAreasDto>()
            }).ToList();


            return (fileFlow, pagination);
        }

        public async Task<FileFlowDto?> GetFolderWithAreas(string ffSrc)
        {

            var result = await _fileFlowRepository.GetFolderWithAreas(ffSrc);
            var areas = await _fileFlowRepository.FileFlowAreas(ffSrc);
            var client = await _orderFormService.GetClientDetailsAsync(result!.OrderID!);
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