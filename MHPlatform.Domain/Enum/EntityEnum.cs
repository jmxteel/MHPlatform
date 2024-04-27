using Installation.Domain.Entities;
using MHPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Enum
{
    public enum EntityEnum
    {
        OrderForm = 1,
        FileFlow = 2,
        FileFlowAreas = 3,
        User = 4,
        Claim = 5,
        RefreshTokens = 6
    }
}
