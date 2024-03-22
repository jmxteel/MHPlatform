using MHPlatform.Domain.Entities;
using MHPlatform.Domain.IRepository;
using MHPlatform.Service.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.IService
{
    public interface ISecurityManagerService
    {
        List<UserClaimDto> GetUserClaims(Guid userId);
        UserAuthBaseDto BuildUserAuthObject(Guid userId, string userName);
        UserAuthBaseDto ValidateUser(string userName, string password);
        RefreshTokenDto? FindByUserIdAsync(string userId);
        Task<UserBaseDto?> GetUserByUserIdAndId(string userId, string username);
    }
}
