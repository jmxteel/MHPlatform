using AutoMapper;
using Installation.Domain.Context;
using Installation.Domain.UOW;
using Installation.Service.IService;
using Installation.Service.Service;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Domain.Repository;
using MHPlatform.Service.IService;
using MHPlatform.Service.Model.Security;
using MHPlatform.Service.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace MHPlatform.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SecurityController: ControllerBase
    {

        private readonly ISecurityManager _securityManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        private SecurityManagerService mgr;
        private AppUserAuthDto _auth;


        public SecurityController(ISecurityManager securityManager
            , IMapper mapper, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _securityManager = securityManager;
            _mapper = mapper;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            this._auth = new AppUserAuthDto();
            this.mgr = new SecurityManagerService(_mapper, _securityManager, _auth, _configuration);
            
        }

        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] AppUser user)
        {
            IActionResult? ret = null;
            var auth = (AppUserAuthDto)mgr.ValidateUser(user.UserName, user.Password);

            if (auth.IsAuthenticated)
            {
                var refreshTokenObject = this.mgr.FindByUserIdAsync(auth.UserId.ToString());

                var refreshTokencredential = await _unitOfWork.GetRepository<RefreshToken>().GetByIdAsync(refreshTokenObject.ID);

                if (string.IsNullOrEmpty(refreshTokencredential!.Token))
                {
                    refreshTokencredential.Token = auth.RefreshToken;
                    await _unitOfWork.GetRepository<RefreshToken>().UpdateAsync(refreshTokencredential!);

                    ret = StatusCode(StatusCodes.Status200OK, auth);
                }

                if (!string.IsNullOrEmpty(refreshTokencredential.Token) && !this.mgr.validateExpiry(refreshTokencredential!.Token))
                {
                    await _unitOfWork.GetRepository<RefreshToken>().UpdateAsync(refreshTokencredential!);
                }
                else
                {
                    auth.RefreshToken = refreshTokencredential.Token;
                }

                ret = StatusCode(StatusCodes.Status200OK, auth);

            }
            else
            {
                ret = StatusCode(StatusCodes.Status404NotFound, "Invalid username or password");
            }

            return ret;
        }

        [HttpPost("RefreshToken/{userName}")]
        public async Task<IActionResult?> RefreshToken([FromBody] RefreshToken refreshToken, string userName)
        {
             IActionResult? ret = null;
            if (!mgr.validateExpiry(refreshToken.Token) || string.IsNullOrEmpty(refreshToken.Token) || string.IsNullOrEmpty(refreshToken.UserId))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }

            var refreshTokenObject = this.mgr.FindByUserIdAsync(refreshToken.UserId);
            if (refreshTokenObject != null && refreshTokenObject.Token == refreshToken.Token)
            {
                var userCredentials = await this.mgr.GetUserByUserIdAndId(refreshToken.UserId, userName);

                var auth = (AppUserAuthDto)mgr.ValidateUser(userCredentials!.UserName, userCredentials.Password);
                if (auth.IsAuthenticated)
                {

                    auth.RefreshToken = refreshTokenObject.Token;
                    ret = StatusCode(StatusCodes.Status200OK, auth);
                }
                else
                {
                    ret = StatusCode(StatusCodes.Status404NotFound, "Invalid credentials");
                }

            }

            return ret;
        }

    }
}
