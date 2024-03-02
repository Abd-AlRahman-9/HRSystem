using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class EmpIncludingDeptAndMngSpecification:GenericSpecification<Employee>
    {
        public EmpIncludingDeptAndMngSpecification()
        {
            Includes.Add(E=>E.Department);
            Includes.Add(E => E.Manager);
        }
        public EmpIncludingDeptAndMngSpecification(int id):base(E=>E.Id == id)
        {
            Includes.Add(E => E.Department);
            Includes.Add(E => E.Manager);
        }
    }
}
