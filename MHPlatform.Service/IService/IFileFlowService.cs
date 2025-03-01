using Installation.Domain.Entities;
using Installation.Service.Model.Installation;
using MHPlatform.Domain.Models;
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
        Task<FileFlowDto?> GetFolderWithAreas(string ffSrc);

        Task<(IEnumerable<FileFlowDto>, PaginationMetaData)> GetFileFlowPaginated(string? filter, string? q, int pageNumber, int pageSize);


    }
}
