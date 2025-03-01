using Installation.Domain.Context;
using Installation.Domain.Entities;
using Installation.Domain.IRepository;
using Installation.Domain.SQLBuilder;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MHPlatform.Domain.Enum;
using MHPlatform.Domain.Models;
using MHPlatform.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Installation.Domain.Repository
{
    public class FileFlowRepository: Generic<FileFlow>, IFileFlowRepository
    {
        private readonly InstallationContext _context;

        public FileFlowRepository(InstallationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<FileFlow>, PaginationMetaData)> GetFileFlowPaginated(string? filter, string? q, int pageNumber, int pageSize)
        {
            //var collection = _context.FileFlow as IQueryable<FileFlow>;
            var collection = (_context.FileFlow ?? Enumerable.Empty<FileFlow>().AsQueryable());

            if (!string.IsNullOrWhiteSpace(filter))
            {
                filter = filter.Trim();
                collection = collection.Where(o => o.OrderID == filter);

            }

            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                collection = collection.Where(c => c.OrderID!.Contains(q)
                || (c.OrderID != null ));
            }

            var totalItemCount = await collection.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

            var fileFlows = await collection.OrderByDescending(a => a.ID)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (fileFlows, paginationMetaData);
        }

        public async Task<FileFlow?> GetFolderWithAreas(string ffSrc)
        {
            var fileFlowQueryBuilder = new QueryBuilder();

            var query = fileFlowQueryBuilder.SQLQueryBuilder<FileFlow>(DataManipulationEnum.SELECT, ffSrc, "1");
            var allFileFlows = await _context.FileFlow!.FromSqlRaw(query).FirstOrDefaultAsync();

            return allFileFlows;
        }

        public async Task<List<FileFlowAreas>> FileFlowAreas(string ffSrc)
        {
            var fileFlowAreas = await _context.FileFlowAreas!.Where(o => o.source == ffSrc && o.Actn == FileFlowEnum.displayed.ToString()).ToListAsync();
            return fileFlowAreas;
        }

    }
}
