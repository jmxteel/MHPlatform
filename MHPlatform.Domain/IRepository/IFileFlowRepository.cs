using Installation.Domain.Entities;
using Installation.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.IRepository
{
    public interface IFileFlowRepository: IGeneric<FileFlow>
    {
        //Task<IEnumerable<FileFlow?>> GetFolderWithAreas(int id);
        Task<FileFlow?> GetFolderWithAreas(int id);
    }
}
