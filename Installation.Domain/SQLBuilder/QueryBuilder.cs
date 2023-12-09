using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installation.Domain.SQLBuilder
{
    public abstract class QueryBuilder<T> where T : class
    {
        public abstract string SQLQueryBuilder(DataManipulationEnum command);
        public abstract string SQLQueryBuilder(DataManipulationEnum command, int id);
    }
}
