using HRDomain.Entities;
using HRDomain.Specification.Params;

namespace HRDomain.Specification.PaginatioCount
{
    public class CountAttendSpecification : GenericSpecification<EmployeeAttendace>
    {
        public CountAttendSpecification(GetAllAttendancesParams P) : base
            (
                A =>
                    A.Deleted == false &&
                    (!(P.From.HasValue && P.To.HasValue) || A.Date >= P.From && A.Date <= P.To)
            )
        { }
    }
}
