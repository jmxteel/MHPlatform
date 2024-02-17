using Installation.Domain.Entities;
using Installation.Service.Model.Installation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Service.IService
{
    public interface IFileFlowService: IGenericService<FileFlowDto,FileFlow>
    {
        //Task<IEnumerable<FileFlowDto?>> GetFolderWithAreas(int id);
        Task<FileFlowDto?> GetFolderWithAreas(int id);

    }
}
