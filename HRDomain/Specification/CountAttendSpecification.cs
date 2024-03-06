using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class CountAttendSpecification:GenericSpecification<EmployeeAttendace>
    {
        public CountAttendSpecification(GetAllAttendancesParams P) : base
            (
                A =>
                    (A.Deleted == false) &&
                    (!((P.From.HasValue) && (P.To.HasValue)) || (A.Date >= P.From) && (A.Date <= P.To))
            )
        { }
    }
}
