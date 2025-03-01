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
using System.Text.Json;

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
        const int maxProductPageSize = 100;

        public FileFlowController(IFileFlowService service, IMapper mapper, InstallationContext context)
        {
            _service = service;
            _mapper = mapper;
            _context = context;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileFlow>>> GetPaginatedFileFlow(string? filter, string? q, int pageNumber, int pageSize)
        {

            try
            {
                if (pageSize > maxProductPageSize)
                {
                    pageSize = maxProductPageSize;
                }

                var (fileFlows, paginationMetaData) = await _service.GetFileFlowPaginated(filter, q, pageNumber, pageSize); ;
                if (fileFlows == null)
                {
                    return NotFound();
                }

                //Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
                Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

                return Ok(fileFlows);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

    }
}
