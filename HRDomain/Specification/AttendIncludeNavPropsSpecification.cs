using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class AttendIncludeNavPropsSpecification:GenericSpecification<EmployeeAttendace>
    {
        public AttendIncludeNavPropsSpecification()
        {
            
        }
        public AttendIncludeNavPropsSpecification(string EmpName,DateOnly date) : base(A=>(A.Deleted==false)&&(A.Date==date)&&(A.Employee.Name==EmpName))
        {
            Includes.Add(A => A.Employee);
            Includes.Add(A=>A.Employee.Department);
            //ThenIncludes.Add(A => A.Employee.Department);
        }
    }
}
