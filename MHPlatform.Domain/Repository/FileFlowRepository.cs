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

namespace Installation.Domain.Repository
{
    public class FileFlowRepository: Generic<FileFlow>, IFileFlowRepository
    {
        private readonly InstallationContext _context;

        public FileFlowRepository(InstallationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FileFlow?> GetFolderWithAreas(string ffSrc)
        {
            var fileFlowQueryBuilder = new QueryBuilder();

            var query = fileFlowQueryBuilder.SQLQueryBuilder<FileFlow>(DataManipulationEnum.SELECT, ffSrc, "1");
            var allFileFlows = await _context.FileFlow!.FromSqlRaw(query).FirstOrDefaultAsync();

            return allFileFlows;
        }

        public async Task<List<FileFlowAreas>> FileFlowAreasList(string ffSrc)
        {
            var fileFlowAreas = await _context.FileFlowAreas!.Where(o => o.source == ffSrc && o.Actn == FileFlowEnum.displayed.ToString()).ToListAsync();
            return fileFlowAreas;
        }
    }
}
