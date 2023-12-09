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


namespace Installation.Domain.Repository
{
    public class FileFlowRepository: Generic<FileFlow>, IFileFlowRepository
    {
        private readonly InstallationContext _context;

        public FileFlowRepository(InstallationContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<FileFlow?>> GetFolderWithAreas(int id)
        public async Task<FileFlow?> GetFolderWithAreas(int id)
        {
            //var fileFlowQueryBuilder = new FileFlowQueryBuilder();
            //var query = fileFlowQueryBuilder.SQLQueryBuilder(DataManipulationEnum.SELECT, id);

            //var allFileFlows = await _context.FileFlow!.FromSqlRaw(query).ToListAsync();
            //// Note: This retrieves all FileFlowAreas;
            //var fileFlowAreas = await _context.FileFlowAreas!.Where(o => o.ffID == id).ToListAsync();

            //var result = allFileFlows.Select(ff => new
            //{
            //    ff.ID,
            //    ff.OrderID,
            //    ff.DesignConsultant,
            //    ff.TehcnicalRep,
            //    Areas = fileFlowAreas.Where(ffa => ffa.ffID == ff.ID)
            //        .Select(ffa => new
            //        {
            //            ffa.ID,
            //            ffa.ffID,
            //            ffa.PArea,
            //            ffa.NumMod
            //        })
            //        .ToList()
            //});

            var fileFlowQueryBuilder = new FileFlowQueryBuilder();
            var query = fileFlowQueryBuilder.SQLQueryBuilder(DataManipulationEnum.SELECT, id);

            //var allFileFlows = await _context.FileFlow!.FromSqlRaw(query).ToListAsync();
            var allFileFlows = await _context.FileFlow!.FromSqlRaw(query).ToListAsync();

            //var fileFlowAreas = await _context.FileFlowAreas!
            //    .Where(o => o.ffID == id)
            //    .ToListAsync();

            List<FileFlowAreas> fileFlowAreas = await FileFlowAreasList(id);

            var result = allFileFlows.Select(ff => new FileFlow
            {
                ID = ff.ID,
                OrderID = ff.OrderID,
                DesignConsultant = ff.DesignConsultant,
                TehcnicalRep = ff.TehcnicalRep,
                Areas = fileFlowAreas
                    .Where(ffa => ffa.ffID == ff.ID)
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

        private Task<List<FileFlowAreas>> FileFlowAreasList(int fileFlowID)
        {
            var fileFlowAreas = _context.FileFlowAreas!.Where(o => o.ffID == fileFlowID).ToListAsync();
            return fileFlowAreas;
        }
    }
}
