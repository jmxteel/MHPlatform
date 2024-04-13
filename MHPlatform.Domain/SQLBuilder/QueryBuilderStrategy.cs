using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.SQLBuilder
{
    public abstract class QueryBuilderStrategy
    {
        public virtual string SQLQueryBuilder<T>(DataManipulationEnum command, string? orderNo, string? topCount = null)
        {
            throw new NotImplementedException();
        }
        public virtual string SQLQueryBuilder(DataManipulationEnum command, string ffSrc)
        {
            throw new NotImplementedException();
        }
    }
}
