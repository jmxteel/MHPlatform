using AutoMapper;
using Installation.Domain;
using Installation.Domain.Context;
using Installation.Domain.Entities;
using Installation.Domain.IRepository;
using Installation.Domain.SQLBuilder;
using Installation.Service.IService;
using Installation.Service.Model.Installation;
using Installation.Service.ServiceHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Installation.API.Controllers
{
    [ApiController]
    [Authorize(Policy = "CanAccessProducts")]
    [Route("api/fileFlow")]
    public class FileFlowController: ControllerBase
    {
        private readonly IFileFlowService _service;
        private readonly IMapper _mapper;
        private readonly InstallationContext _context;

        public FileFlowController(IFileFlowService service, IMapper mapper, InstallationContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileFlowDto>>> All()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }

        [HttpGet("Areas/{ffSrc}")]
        public async Task<ActionResult<IEnumerable<FileFlow>>> FolderWithAreas(string ffSrc)
        {
            //var fileFlowQueryBuilder = new FileFlowQueryBuilder();
            //var query = fileFlowQueryBuilder.SQLQueryBuilder(DataManipulationEnum.SELECT, id);

            //var allFileFlows = await _context.FileFlow!.FromSqlRaw(query).ToListAsync();

            //var fileFlowAreas = await _context.FileFlowAreas!.Where(o => o.ffID == allFileFlows[0].ID).ToListAsync(); // Note: This retrieves all FileFlowAreas;

            //var result = allFileFlows.Select(ff => new
            //{
            //    ff.ID,
            //    ff.OrderID,
            //    ff.DesignConsultant,
            //    ff.TehcnicalRep,
            //    Areas = fileFlowAreas.Where(ffa => ffa.ffID == ff.ID)
            //        .Select(ffa => new
            //        {
            //            ffa.ID,
            //            ffa.ffID,
            //            ffa.PArea,
            //            ffa.NumMod
            //        })
            //        .ToList()
            //});
            var result = await _service.GetFolderWithAreas(ffSrc);
            return Ok(result);

        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<FileFlow>> Get(int id)
        //{
        //    var result = await _movie.GetByIdAsync(id);
        //    return Ok(result);
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<FileFlowDto>> Get(int? id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return NotFound(message);
            }
        }

    }
}
