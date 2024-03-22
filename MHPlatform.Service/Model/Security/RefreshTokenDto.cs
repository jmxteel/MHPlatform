using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Model.Security
{
    public class RefreshTokenDto
    {
        [Key()]
        public int ID { get; set; }
        [Required()]
        public string UserId { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
    }
}
