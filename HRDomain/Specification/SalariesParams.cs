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
        public string? Sort { get; set; }
        public string? Search { get; set; }
    }
}
