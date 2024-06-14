using MHPlatform.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MHPlatform.API.Controllers
{
    [Route("api/Client")]
    //[Authorize(Policy = "CanAccessProducts")]
    [ApiController]
    public class ClientController: ControllerBase   
    {
        private readonly IOrderFormService _service;
        const int maxProductPageSize = 20;
        public ClientController(IOrderFormService service)
        {
            this._service = service;
        }

        [HttpGet("{ordrNo}")]
        public async Task<IActionResult> GetClient(string ordrNo)
        {
            try
            {
                var client = await _service.GetClientDetails(ordrNo);
                return Ok(client);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }

        [HttpGet("AllClients")]
        public async Task<IActionResult> GetAllClients(string? filter, string? q, int pageNumber = 1, int pageSize = 10)
        {

            try
            {
                if (pageSize > maxProductPageSize)
                {
                    pageSize = maxProductPageSize;
                }

                var (clients, paginationMetaData) = await _service.GetAllClientAsync(filter, q, pageNumber, pageSize);
                if (clients == null)
                {
                    return NotFound();
                }

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

                return Ok(clients);

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

        }
    }
}
