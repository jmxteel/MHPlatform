using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHPlatform.Service.Model
{
    public class PagedResponse
    {
        public List<Person>? Rows { get; set; }
        public int TotalCount { get; set; }
    }
}
