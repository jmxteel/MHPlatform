using AutoMapper;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Domain.Repository;
using MHPlatform.Service.IService;
using MHPlatform.Service.Model.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Service
{
    public class SecurityManagerService: ISecurityManagerService
    {
        private readonly IMapper _mapper;
        private readonly ISecurityManager _securityManager;
        private UserAuthBaseDto _auth;

        public SecurityManagerService(IMapper mapper, ISecurityManager securityManager, UserAuthBaseDto auth)
        {
            _mapper = mapper;
            _securityManager = securityManager;
            _auth = auth;
        }

        public List<UserClaimDto> GetUserClaims(Guid userId)
        {
            List<UserClaimDto> list = new List<UserClaimDto>();

            try
            {
                //list = _context.Claims!.Where(p => p.UserId == userId).ToList();
                var userClaims = _securityManager.GetUserClaims(userId);
                list = _mapper.Map<List<UserClaimDto>>(userClaims);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to retrieve user claims", ex);
            }

            return list;
        }

        public UserAuthBaseDto BuildUserAuthObject(Guid userId, string userName)
        {
            List<UserClaimDto> claims = new List<UserClaimDto>();
            Type _authType = _auth.GetType();

            // Set user properties
            _auth.UserId = userId;
            _auth.UserName = userName;
            _auth.IsAuthenticated = true;

            // Get all claims for this user
            claims = GetUserClaims(userId);

            foreach (UserClaimDto claim in claims)
            {
                try
                {
                    // use reflection to set property
                    _authType.GetProperty(claim.ClaimType)?.SetValue(_auth, Convert.ToBoolean(claim.ClaimValue), null);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error in setting claims", ex);
                }
                var sas = _auth;
            }

            return _auth;
        }

        public UserAuthBaseDto ValidateUser(string userName, string password)
        {
            List<UserBaseDto> list = new List<UserBaseDto>();

            try
            {

                var user = _securityManager.GetUser(userName, password);
                list = _mapper.Map<List<UserBaseDto>>(user);

                if (list.Count > 0)
                {
                    _auth = BuildUserAuthObject(list[0].UserId, userName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Exception while trying to retrieve user", ex);
            }

            return _auth;
        }

    }
}

