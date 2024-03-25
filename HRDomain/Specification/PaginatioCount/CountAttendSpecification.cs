using HRDomain.Entities;
using HRDomain.Specification.Params;

namespace HRDomain.Specification.PaginatioCount
{
    public class CountAttendSpecification : GenericSpecification<EmployeeAttendace>
    {
        public CountAttendSpecification(GetAllAttendancesParams P) : base
            (
                A =>
                    A.Deleted == false
                    &&
                    (
                        !(P.To.HasValue) ||
                        (A.Date >= new DateOnly(P.Year, P.From, 1)) &&
                        (A.Date <= new DateOnly(P.Year, P.To.Value, DateTime.DaysInMonth(P.Year, P.To.Value)))
                    )
                    &&
                    ((P.To.HasValue) || (A.Date >= new DateOnly(P.Year, P.From, 1)))
            //&&
            //(
            //    string.IsNullOrEmpty(P.Search) ||
            //    (A.Employee.Name.ToLower().Contains($"{P.Search}".ToLower()) ||
            //    A.Employee.Department.Name.ToLower().Contains($"{P.Search}".ToLower()))
            //)
            )
        { }
    }
}
