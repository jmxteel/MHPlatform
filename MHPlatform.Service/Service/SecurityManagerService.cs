using AutoMapper;
using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Domain.Repository;
using MHPlatform.Service.IService;
using MHPlatform.Service.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Installation.Service.IService;
using Installation.Service.Service;
using Installation.Domain.IRepository;
using System.Linq.Expressions;
using Installation.Domain.UOW;

namespace MHPlatform.Service.Service
{
    public class SecurityManagerService: ISecurityManagerService
    {
        private readonly IMapper _mapper;
        private readonly ISecurityManager _securityManager;
        private readonly IConfiguration _configuration;
        private UserAuthBaseDto _auth;

        public SecurityManagerService(IMapper mapper, ISecurityManager securityManager
            , UserAuthBaseDto auth, IConfiguration configuration)
        {
            _mapper = mapper;
            _securityManager = securityManager;
            _auth = auth;
            _configuration = configuration;
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
            }
            var tokens = BuildJwtToken(claims, userName);
            //_auth.BearerToken = BuildJwtToken(claims, userName);
            _auth.BearerToken = tokens.Item1; // Bearer Token
            _auth.RefreshToken = tokens.Item2; // Refresh Token

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

        protected Tuple<string,string> BuildJwtToken(IList<UserClaimDto> claims, string userName)
        {
            JwtSettings settings = GetJwtSettings(_configuration);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));

            //Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //Add custom claims
            foreach (UserClaimDto claim in claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: jwtClaims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(settings.MinutesToExpiration),
                signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
                );

            var refreshToken = BuildJwtRefreshToken();

            //Create a string representation of Jwt Token
            //return new JwtSecurityTokenHandler().WriteToken(token);

            return Tuple.Create(new JwtSecurityTokenHandler().WriteToken(token), refreshToken);
        }

        protected JwtSettings GetJwtSettings(IConfiguration configuration)
        {
            JwtSettings settings = new JwtSettings();
            settings.Key = configuration["JwtToken:key"];
            settings.Issuer = configuration["JwtToken:issuer"];
            settings.Audience = configuration["JwtToken:audience"];
            settings.MinutesToExpiration = Convert.ToInt32(configuration["JwtToken:minutestoexpiration"]);

            return settings;
        }

        protected string BuildJwtRefreshToken()
        {
            JwtSettings settings = GetRefreshJwtSettings(_configuration!);
            SymmetricSecurityKey refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)); // Assuming a separate key for refresh tokens
            var refreshToken = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(settings.MinutesToExpiration), // Typically longer expiration for refresh tokens
                signingCredentials: new SigningCredentials(refreshKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }
        protected JwtSettings GetRefreshJwtSettings(IConfiguration configuration)
        {
            JwtSettings settings = new JwtSettings();
            settings.Key = configuration["RefreshJwtToken:key"];
            settings.Issuer = configuration["RefreshJwtToken:issuer"];
            settings.Audience = configuration["RefreshJwtToken:audience"];
            settings.MinutesToExpiration = Convert.ToInt32(configuration["RefreshJwtToken:minutestoexpiration"]);

            return settings;
        }

        public RefreshTokenDto GetRefreshToken(Guid userId)
        {

            var result = FindByUserIdAsync(userId.ToString("N"));

            return result;
        }

        public RefreshTokenDto FindByUserIdAsync(string userId)
        {
            var userIds = userId.Substring(0, userId.Length - 0).ToString();
            var result = _securityManager.FindByUserIdAsync(userIds);
            var resultDto = _mapper.Map<RefreshTokenDto>(result);            

            return resultDto;
        }

        public async Task<UserBaseDto?> GetUserByUserIdAndId(string userId, string username)
        {

            try
            {
              var result = await _securityManager.GetUserByUserIdAndId(userId, username);

              return _mapper.Map<UserBaseDto>(result);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error when fetching data", ex.Message);
            }

        }

        public bool validateExpiry(string? refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var securityToken = tokenHandler.ReadToken(refreshToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                var payload = jwtSecurityToken?.Payload;
                var expiry = payload?.FirstOrDefault(x => x.Key == "exp");
                var expiryDate = (long)payload!["exp"];

                var unixTimestampNow = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();

                if (unixTimestampNow < expiryDate)
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentNullException("Refresh token is null", ex.Message);
            }


            return false;
        }

    }
}

