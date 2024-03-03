using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class EmpVacsNavPropsSpecification:GenericSpecification<EmployeeVacation>
    {
        public EmpVacsNavPropsSpecification()
        {
            Includes.Add(EmpVac=>EmpVac.Employee);
            Includes.Add(EmpVac => EmpVac.Vacation);
        }
        public EmpVacsNavPropsSpecification(int EmpId,int VacId):base(EmpVac=>(EmpVac.EmployeeId==EmpId)&&(EmpVac.VacationId==VacId))
        {
            Includes.Add(EmpVac=>EmpVac.Employee);
            Includes.Add(EmpVac => EmpVac.Vacation);
        }
    }
}
