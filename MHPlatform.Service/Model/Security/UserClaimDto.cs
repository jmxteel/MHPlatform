using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Model.Security
{
    public class UserClaimDto
    {
        [Required()]
        [Key()]
        public Guid ClaimId { get; set; }

        [Required()]
        public Guid UserId { get; set; }

        [Required()]
        public string ClaimType { get; set; } = string.Empty;

        [Required()]
        public string ClaimValue { get; set; } = string.Empty;
    }
}
