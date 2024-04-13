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
            try
            {
                var result = await _service.GetFolderWithAreas(ffSrc);
                return Ok(result);
            }
            catch
            {
                return NotFound($"The FFSrc {ffSrc} does not exist");
            }

        }

    }
}
