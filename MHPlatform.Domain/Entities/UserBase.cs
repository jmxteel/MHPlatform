using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Domain.Entities
{
    [Table("User", Schema ="Security")]
    public class UserBase
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
