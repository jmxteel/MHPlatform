using MHPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.IRepository
{
    public interface ISecurityManager
    {
        List<UserClaim> GetUserClaims(Guid userId);
        UserAuthBase BuildUserAuthObject(Guid userId, string userName);
        UserAuthBase ValidateUser(string userName, string password);
        List<UserBase> GetUser(string userName, string password);
    }
}
