using Installation.Service.IService;
using Installation.Service.Model.Installation;
using Microsoft.AspNetCore.Mvc;

namespace Installation.API.Controllers
{
    [Route("api/FileFlowAreas")]
    [ApiController]
    public class FileFlowAreasController: ControllerBase
    {
        private readonly IFileFlowAreaService _service;

        public FileFlowAreasController(IFileFlowAreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileFlowAreasDto>>> All()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }
    }
}
