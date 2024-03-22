using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Entities
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;

        public string RefreshKey { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int MinutesToExpiration { get; set; }
    }
}
