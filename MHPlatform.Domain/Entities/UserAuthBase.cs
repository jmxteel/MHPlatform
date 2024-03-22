using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Entities
{
    public class UserAuthBase
    {
        public UserAuthBase() 
        {
            UserId = Guid.NewGuid();
            UserName = string.Empty;
            BearerToken = string.Empty;
            RefreshToken = string.Empty;
            IsAuthenticated = false;
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
