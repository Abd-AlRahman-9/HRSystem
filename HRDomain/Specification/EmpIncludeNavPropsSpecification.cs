using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class EmpIncludeNavPropsSpecification:GenericSpecification<Employee>
    {
        public EmpIncludeNavPropsSpecification()
        {
            Includes.Add(E=>E.Department);
            Includes.Add(E => E.Manager);
        }
        public EmpIncludeNavPropsSpecification(int id):base(E=>E.Id == id)
        {
            Includes.Add(E => E.Department);
            Includes.Add(E => E.Manager);
        }
    }
}
