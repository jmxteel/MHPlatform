using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Model.Security
{
    public class UserBaseDto
    {
        [Required()]
        [Key()]
        public Guid UserId { get; set; }

        [Required()]
        public string UserName { get; set; } = string.Empty;

        [Required()]
        public string Password { get; set; } = string.Empty;

    }
}
