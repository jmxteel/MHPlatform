using MHPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Model.Security
{
    public class AppUserAuthDto: UserAuthBaseDto
    {
        public AppUserAuthDto() : base()
        {
            this.CanAccessProducts = false;
            this.CanAccessCategories = false;
            this.CanAccessLogs = false;
            this.CanAccessSettings = false;
            this.CanAddProduct = false;
            this.CanAddCategory = false;
            this.CanSaveProduct = false;
        }

        public bool CanAccessProducts { get; set; }
        public bool CanAccessCategories { get; set; }
        public bool CanAccessLogs { get; set; }
        public bool CanAccessSettings { get; set; }
        public bool CanAddProduct { get; set; }
        public bool CanAddCategory { get; set; }
        public bool CanSaveProduct { get; set; }

    }
}

