using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Specification
{
    public class SalariesParams
    {
        public int StartMonth { get; set; }
        public int Year {  get; set; }
        public int? EndMonth { get; set; }
        public string? Search { get; set; }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > 10 ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;
    }
}
