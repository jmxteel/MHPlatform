using Installation.Domain.Entities;
using MHPlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.Context
{
    public class InstallationContext : DbContext
    {
        public DbSet<FileFlow>? FileFlow { get; set; }
        public DbSet<FileFlowAreas>? FileFlowAreas { get; set; }
        public DbSet<UserBase>? Users { get; set; }
        public DbSet<UserClaim>? Claims { get; set; }
        public InstallationContext(DbContextOptions<InstallationContext> options) : base(options)
        {
        }

    }
}
