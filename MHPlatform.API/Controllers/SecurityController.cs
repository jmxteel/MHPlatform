using AutoMapper;
using Installation.Domain.Context;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Domain.Repository;
using MHPlatform.Service.IService;
using MHPlatform.Service.Model.Security;
using MHPlatform.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace MHPlatform.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SecurityController: ControllerBase
    {

        private readonly ISecurityManager _securityManager;
        private readonly IMapper _mapper;

        private SecurityManagerService mgr;
        private AppUserAuthDto _auth;


        public SecurityController(ISecurityManagerService service, ISecurityManager securityManager, IMapper mapper)
        {
            _securityManager = securityManager;
            _mapper = mapper;
            this._auth = new AppUserAuthDto();
            this.mgr = new SecurityManagerService(_mapper, _securityManager, _auth);
        }

        [HttpPost]
        public IActionResult LogIn([FromBody] AppUser user)
        {
            IActionResult? ret = null;
            var auth = (AppUserAuthDto)mgr.ValidateUser(user.UserName, user.Password);

            if (auth.IsAuthenticated)
            {
                ret = StatusCode(StatusCodes.Status200OK, auth);
            }
            else
            {
                ret = StatusCode(StatusCodes.Status404NotFound, "Invalid username or password");
            }

            return ret;
        }

    }
}
