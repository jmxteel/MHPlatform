using Installation.Domain.Context;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Repository
{
    public class SecurityManager: ISecurityManager
    {
        private readonly InstallationContext _context;
        private UserAuthBase _auth;

        public SecurityManager(InstallationContext context, UserAuthBase auth)
        {
            _context = context;
            _auth = auth;
        }

        public List<UserClaim> GetUserClaims(Guid userId)
        {
            List<UserClaim> list = new List<UserClaim>();

            try
            {
                list = _context.Claims!.Where(p => p.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception trying to retrieve user claims", ex);
            }

            return list;
        }

        public UserAuthBase BuildUserAuthObject(Guid userId, string userName)
        {
            List<UserClaim> claims = new List<UserClaim>();
            Type _authType = _auth.GetType();

            // Set user properties
            _auth.UserId = userId;
            _auth.UserName = userName;
            _auth.IsAuthenticated = true;

            // Get all claims for this user
            claims = GetUserClaims(userId);

            // Loop through all claims
            // set properties of user object
            foreach (UserClaim claim in claims)
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


        public UserAuthBase ValidateUser(string userName, string password)
        {
            List<UserBase> list = new List<UserBase>();

            try
            {
                list = _context.Users!.Where(
                    u => u.UserName.ToLower() == userName.ToLower()
                    && u.Password.ToLower() ==
                    password.ToLower()).ToList();

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

        public List<UserBase> GetUser(string userName, string password)
        {
            List<UserBase> list = new List<UserBase>();
            try
            {
                 list = _context.Users!.Where(
                        u => u.UserName.ToLower() == userName.ToLower()
                        && u.Password.ToLower() ==
                        password.ToLower()).ToList();
            } catch (Exception ex)
            {
                throw new Exception("Error when getting the user", ex);
            }

            return list;
        }

        public RefreshToken? FindByUserIdAsync(string userId)
        {
            var result =  _context.RefreshTokens!.Where(o => o.UserId == userId).FirstOrDefault();

            return result;
        }

        public async Task<UserBase?> GetUserByUserIdAndId(string userId, string username)
        {
            var result = await _context.Users!.Where(o => o.UserId == new Guid(userId) && o.UserName == username).FirstOrDefaultAsync();

            return (UserBase?)result;
        }
    }

}


