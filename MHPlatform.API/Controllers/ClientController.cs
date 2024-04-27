using MHPlatform.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MHPlatform.API.Controllers
{
    [Route("api/Client")]
    //[Authorize(Policy = "CanAccessProducts")]
    [ApiController]
    public class ClientController: ControllerBase   
    {
        private readonly IOrderFormService _service;
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
    }
}
