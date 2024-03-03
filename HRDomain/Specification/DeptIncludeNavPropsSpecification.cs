using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class DeptIncludeNavPropsSpecification:GenericSpecification<Department>
    {
        public DeptIncludeNavPropsSpecification()
        {
            Includes.Add(E => E.Manager);
            //Includes.Add(E => E.Employees);
        }
        public DeptIncludeNavPropsSpecification(int id):base(E=>E.Id==id)
        {
            Includes.Add(E => E.Manager);
            //Includes.Add(E => E.Employees);
        }
    }
}
