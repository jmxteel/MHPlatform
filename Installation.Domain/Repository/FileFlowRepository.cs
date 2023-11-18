using Installation.Domain.Context;
using Installation.Domain.Entities;
using Installation.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.Repository
{
    public class FileFlowRepository: Generic<FileFlow>, IFileFlowRepository
    {
        public FileFlowRepository(InstallationContext context): base(context)
        {

        }
    }
}
