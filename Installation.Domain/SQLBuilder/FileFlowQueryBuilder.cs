using Installation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Installation.Domain.SQLBuilder
{
    public class FileFlowQueryBuilder : QueryBuilder<FileFlow>
    {
        public override string SQLQueryBuilder(DataManipulationEnum command)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(command.ToString());

            foreach (var property in typeof(FileFlow).GetProperties())
            {
                //Console.WriteLine($"Property name: {property.Name}, Property type: {property.PropertyType}");
                queryBuilder.AppendLine($" {property.Name},");
            }
            queryBuilder.AppendLine(" FROM");
            queryBuilder.AppendLine($" FileFlow");


            return queryBuilder.ToString();
        }
        public override string SQLQueryBuilder(DataManipulationEnum command, int id)
        {

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(command.ToString());
            queryBuilder.AppendLine("[ID],");
            queryBuilder.AppendLine("[OrderID],");
            queryBuilder.AppendLine("[FileFlowNo],");
            queryBuilder.AppendLine("[datecreated],");
            queryBuilder.AppendLine("[OTfactor],[OTchkbx],");
            queryBuilder.AppendLine("[Priority],");
            queryBuilder.AppendLine("[ApprxDelDate],");
            queryBuilder.AppendLine("[ProjectList],");
            queryBuilder.AppendLine("[ApprDelDateTxt],");
            queryBuilder.AppendLine("[PaymentReceived],");
            queryBuilder.AppendLine("[FileMadeUp],");
            queryBuilder.AppendLine("[DesignConsultant],");
            queryBuilder.AppendLine("[TehcnicalRep],");
            queryBuilder.AppendLine("[FileIn],");
            queryBuilder.AppendLine("[CMDate],");
            queryBuilder.AppendLine("[TargetDate],");
            queryBuilder.AppendLine("[FileOut],");
            queryBuilder.AppendLine("[OverTargetDate],");
            queryBuilder.AppendLine("[reasons],");
            queryBuilder.AppendLine("[LeadStart],");
            queryBuilder.AppendLine("[LeadClosed],");
            queryBuilder.AppendLine("[Revision],");
            queryBuilder.AppendLine("[chckddocsDate],");
            queryBuilder.AppendLine("[chckdforTechnclDate],");
            queryBuilder.AppendLine("[ApprovedDate],");
            queryBuilder.AppendLine("[leftshwroom],");
            queryBuilder.AppendLine("[recvinplant],");
            queryBuilder.AppendLine("[shwrm],");
            queryBuilder.AppendLine("[deleted],");
            queryBuilder.AppendLine("[deletedby],");
            queryBuilder.AppendLine("[GrpngSysGen],");
            queryBuilder.AppendLine("[GrpngCtgry],");
            queryBuilder.AppendLine("[GrpngMat],");
            queryBuilder.AppendLine("[ManualGrpngCtgry],");
            queryBuilder.AppendLine("[ManualGrpngMat],");
            queryBuilder.AppendLine("[chckbySalesDesigner],");
            queryBuilder.AppendLine("[variation],");
            queryBuilder.AppendLine("[workingDaysOver],");
            queryBuilder.AppendLine("[workingDaysUnder],");
            queryBuilder.AppendLine("[FFsrc],");
            queryBuilder.AppendLine("[FGrouping],");
            queryBuilder.AppendLine("[MGrouping],");
            queryBuilder.AppendLine("[SysVer],");
            queryBuilder.AppendLine("[typ],");
            queryBuilder.AppendLine("[cbfpaging],");
            queryBuilder.AppendLine("[fpaging],");
            queryBuilder.AppendLine("[lock],");
            queryBuilder.AppendLine("[lockedby],");
            queryBuilder.AppendLine("[MHGrouping],");
            queryBuilder.AppendLine("[samplecolor]");

            //foreach (var property in typeof(FileFlow).GetProperties())
            //{
            //            if (property.Name == "islock") { property.SetValue(property.Name, "lock"); };
            //            //Console.WriteLine($"Property name: {property.Name}, Property type: {property.PropertyType}");
            //            queryBuilder.AppendLine($" [{property.Name}]");
            //}
            queryBuilder.AppendLine(" FROM");
            queryBuilder.AppendLine($" FileFlow");
            queryBuilder.AppendLine($" WHERE ID = {id} AND (deleted IS NULL OR deleted = '')");


            return queryBuilder.ToString();
        }
    }
}
