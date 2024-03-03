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
        public DeptIncludeNavPropsSpecification(string sort)
        {
            Includes.Add(E => E.Manager);
            //Includes.Add(E => E.Employees);
            switch (sort)
            {
                case "WorkDaysAsc":
                    AddOrderBy(E => E.WorkDays);
                    break;
                case "WorkDaysDesc":
                    AddOrderByDescending(E => E.WorkDays);
                    break;
                default:
                    AddOrderBy(E => E.Name);
                    break;
            }
        }
        public DeptIncludeNavPropsSpecification(int id):base(E=>E.Id==id)
        {
            Includes.Add(E => E.Manager);
            //Includes.Add(E => E.Employees);
        }
    }
}
