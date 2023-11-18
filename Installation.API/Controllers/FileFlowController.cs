using Installation.Domain.Entities;
using Installation.Domain.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Installation.API.Controllers
{
    [ApiController]
    [Route("api/fileFlow")]
    public class FileFlowController: ControllerBase
    {
        private readonly IFileFlowRepository _movie;

        public FileFlowController(IFileFlowRepository movie)
        {
            _movie = movie;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileFlow>>> All()
        {
           var result = await _movie.GetAllAsync();
           
           return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileFlow>> Get(int id)
        {
            var result = await _movie.GetByIdAsync(id);
            return Ok(result);
        }

    }
}
