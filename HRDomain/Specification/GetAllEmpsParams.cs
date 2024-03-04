using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Specification
{
    public class GetAllEmpsParams:IPagination
    {
        public int? MngId { get; set; }
        public int? DeptId { get; set; }
        public string sort { get; set; }
        public bool IsPaginationEnabled { get; set; }
        public int? PageSize { get; set; }
        public int? PageCount { get; set;}
    }
}
