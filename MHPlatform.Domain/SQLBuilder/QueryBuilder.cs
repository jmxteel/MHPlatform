using Installation.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Installation.Domain.SQLBuilder
{
    public class QueryBuilder : QueryBuilderStrategy
    {
        public override string SQLQueryBuilder<T>(DataManipulationEnum command, string? orderNo, string? topCount = null)
        {
            Type entity = typeof(T);
            var entityName = entity.Name.ToString();
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(command.ToString());

            if(topCount is not null)
            {
                queryBuilder.AppendLine($" TOP {topCount}");
            }

            int propertyCount = 0;
            foreach (var property in typeof(T).GetProperties())
            {
                propertyCount++;
                _= propertyCount != typeof(T).GetProperties().Length? queryBuilder.AppendLine($" {property.Name},") : queryBuilder.AppendLine($" {property.Name}");
                //Console.WriteLine($"Property name: {property.Name}, Property type: {property.PropertyType}");
                //queryBuilder.AppendLine($" {property.Name},");
            }

            queryBuilder.AppendLine(" FROM");
            queryBuilder.AppendLine($" {entityName}");

            if(entityName == "OrderForm")
            {
                queryBuilder.AppendLine($" WHERE Ordrno = '{orderNo}' AND del = 'no'");
            }

            return queryBuilder.ToString();
        }
        public override string SQLQueryBuilder(DataManipulationEnum command, string ffSrc)
        {

            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine(command.ToString());
            queryBuilder.AppendLine("ofrm.[Ordrno],");
            queryBuilder.AppendLine("ofrm.[Ctitle],");
            queryBuilder.AppendLine("ofrm.[CName],");
            queryBuilder.AppendLine("ofrm.[CSurname],");
            queryBuilder.AppendLine("ofrm.[CAdd],");
            queryBuilder.AppendLine("ofrm.[CCon],");
            queryBuilder.AppendLine("ofrm.[Cmobile],");
            queryBuilder.AppendLine("ofrm.[CFax],");
            queryBuilder.AppendLine("ofrm.[ConPrsn],");
            queryBuilder.AppendLine("ff.[ID],");
            queryBuilder.AppendLine("ff.[OrderID],");
            queryBuilder.AppendLine("ff.[FileFlowNo],");
            queryBuilder.AppendLine("ff.[datecreated],");
            queryBuilder.AppendLine("ff.[OTfactor],[OTchkbx],");
            queryBuilder.AppendLine("ff.[Priority],");
            queryBuilder.AppendLine("ff.[ApprxDelDate],");
            queryBuilder.AppendLine("ff.[ProjectList],");
            queryBuilder.AppendLine("ff.[ApprDelDateTxt],");
            queryBuilder.AppendLine("ff.[PaymentReceived],");
            queryBuilder.AppendLine("ff.[FileMadeUp],");
            queryBuilder.AppendLine("ff.[DesignConsultant],");
            queryBuilder.AppendLine("ff.[TehcnicalRep],");
            queryBuilder.AppendLine("ff.[FileIn],");
            queryBuilder.AppendLine("ff.[CMDate],");
            queryBuilder.AppendLine("ff.[TargetDate],");
            queryBuilder.AppendLine("ff.[FileOut],");
            queryBuilder.AppendLine("ff.[OverTargetDate],");
            queryBuilder.AppendLine("ff.[reasons],");
            queryBuilder.AppendLine("ff.[LeadStart],");
            queryBuilder.AppendLine("ff.[LeadClosed],");
            queryBuilder.AppendLine("ff.[Revision],");
            queryBuilder.AppendLine("ff.[chckddocsDate],");
            queryBuilder.AppendLine("ff.[chckdforTechnclDate],");
            queryBuilder.AppendLine("ff.[ApprovedDate],");
            queryBuilder.AppendLine("ff.[leftshwroom],");
            queryBuilder.AppendLine("ff.[recvinplant],");
            queryBuilder.AppendLine("ff.[shwrm],");
            queryBuilder.AppendLine("ff.[deleted],");
            queryBuilder.AppendLine("ff.[deletedby],");
            queryBuilder.AppendLine("ff.[GrpngSysGen],");
            queryBuilder.AppendLine("ff.[GrpngCtgry],");
            queryBuilder.AppendLine("ff.[GrpngMat],");
            queryBuilder.AppendLine("ff.[ManualGrpngCtgry],");
            queryBuilder.AppendLine("ff.[ManualGrpngMat],");
            queryBuilder.AppendLine("ff.[chckbySalesDesigner],");
            queryBuilder.AppendLine("ff.[variation],");
            queryBuilder.AppendLine("ff.[workingDaysOver],");
            queryBuilder.AppendLine("ff.[workingDaysUnder],");
            queryBuilder.AppendLine("ff.[FFsrc],");
            queryBuilder.AppendLine("ff.[FGrouping],");
            queryBuilder.AppendLine("ff.[MGrouping],");
            queryBuilder.AppendLine("ff.[SysVer],");
            queryBuilder.AppendLine("ff.[typ],");
            queryBuilder.AppendLine("ff.[cbfpaging],");
            queryBuilder.AppendLine("ff.[fpaging],");
            queryBuilder.AppendLine("ff.[lock],");
            queryBuilder.AppendLine("ff.[lockedby],");
            queryBuilder.AppendLine("ff.[MHGrouping],");
            queryBuilder.AppendLine("ff.[samplecolor]");
            queryBuilder.AppendLine(" FROM");
            queryBuilder.AppendLine($" FileFlow ff");
            queryBuilder.AppendLine($" LEFT JOIN OrderForm ofrm ON ff.OrderID = ofrm.Ordrno");
            queryBuilder.AppendLine($" WHERE ff.FFsrc = '{ffSrc}' AND (ff.deleted IS NULL OR ff.deleted = '')");


            return queryBuilder.ToString();
        }
    }
}
