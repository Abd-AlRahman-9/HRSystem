using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Specification.Params
{
    public class SalProcedureParams
    {
        public string NationalId { get; set; }
        public int StartMonth { get; set; }
        public int Year { get; set; }
        public int? EndMonth { get; set; }
    }
}
