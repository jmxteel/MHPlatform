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
using MHPlatform.Domain;

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
            var fileFlowQueryBuilder = new FileFlowQueryBuilder();
            var query = fileFlowQueryBuilder.SQLQueryBuilder(DataManipulationEnum.SELECT, ffSrc);

            var allFileFlows = await _context.FileFlow!.FromSqlRaw(query).ToListAsync();

            List<FileFlowAreas> fileFlowAreas = await FileFlowAreasList(ffSrc);

            var result = allFileFlows.Select(ff => new FileFlow
            {
                ID = ff.ID,
                OrderID = ff.OrderID,
                DesignConsultant = ff.DesignConsultant,
                TehcnicalRep = ff.TehcnicalRep,
                Areas = fileFlowAreas
                    .Where(ffa => ffa.source == ff.FFsrc)
                    .Select(ffa => new FileFlowAreas
                    {
                        ID = ffa.ID,
                        ffID = ffa.ffID,
                        PArea = ffa.PArea,
                        NumMod = ffa.NumMod
                    }
                    )
                    .ToList()
            }).FirstOrDefault();

            return result;
        }

        private Task<List<FileFlowAreas>> FileFlowAreasList(string ffSrc)
        {
            var fileFlowAreas = _context.FileFlowAreas!.Where(o => o.source == ffSrc && o.Actn == FileFlowEnum.displayed.ToString()).ToListAsync();
            return fileFlowAreas;
        }
    }
}
