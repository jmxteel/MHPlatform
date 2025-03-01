using Installation.Domain.Entities;
using Installation.Domain.Repository;
using MHPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.IRepository
{
    public interface IFileFlowRepository: IGeneric<FileFlow>
    {
        Task<FileFlow?> GetFolderWithAreas(string ffSrc);

        Task<List<FileFlowAreas>> FileFlowAreas(string ffSrc);

        Task<(IEnumerable<FileFlow>, PaginationMetaData)> GetFileFlowPaginated(string? filter, string? q, int pageNumber, int pageSize);

    }
}
