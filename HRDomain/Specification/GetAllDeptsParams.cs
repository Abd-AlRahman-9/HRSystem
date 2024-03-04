using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Specification
{
    public class GetAllDeptsParams : IPagination
    {
        public string sort { get; set; }
        public int MngId { get; set; }
        public bool IsPaginationEnabled { get; set; }
        public int? PageSize { get; set; }
        public int? PageCount { get; set; }
    }
}
