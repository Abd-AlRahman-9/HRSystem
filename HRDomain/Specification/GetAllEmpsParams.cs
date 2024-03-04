using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Specification
{
    public class GetAllEmpsParams
    {
        public int? MngId { get; set; }
        public int? DeptId { get; set; }
        public string sort { get; set; }

        private int? pageSize;
        public int? PageSize 
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int? PageCount { get; set; } = 1;
    }
}
