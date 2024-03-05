using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class CountSetSpecification:GenericSpecification<Department>
    {
        public CountSetSpecification(GetAllDeptsParams P) : base
            (
                D => (D.Deleted == true) && (D.Name == null)
            )
        {        }
    }
}
