using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class AttendIncludeNavPropsSpecification:GenericSpecification<EmployeeAttendace>
    {
        public AttendIncludeNavPropsSpecification(GetAllAttendancesParams P) : base
            (
                A =>
                    (A.Deleted == false) &&
                    (!((P.From.HasValue)&&(P.To.HasValue)) ||  (A.Date >= P.From) && (A.Date <= P.To))&&
                    (string.IsNullOrEmpty(P.Search) || A.Employee.Name.ToLower().Contains(P.Search))  
            )
        {
            Includes.Add(A=>A.Employee);
            Includes.Add(A=>A.Employee.Department);

<<<<<<< HEAD
            if (P == null)
                P = new GetAllAttendancesParams() { PageSize = 10, PageIndex = 1 };

            ApplyPagination(P.PageSize * (P.PageIndex - 1), P.PageSize);

=======
            ApplyPagination(P.PageSize * (P.PageIndex - 1), P.PageSize);
>>>>>>> 66158759051a83b6a1367ff9dc227c516b59c044

            if (!string.IsNullOrEmpty(P.sort))
            {
                switch (P.sort)
                {
                    case "DateAsc":
                        AddOrderBy(A => A.Date);
                        break;
                    case "DateDesc":
                        AddOrderByDescending(A=>A.Date);
                        break;
                    default:
                        AddOrderByDescending(A=>A.Discount);
                        break;
                }
            }
        }
        public AttendIncludeNavPropsSpecification(string EmpName,DateOnly date) : base(A=>(A.Deleted==false)&&(A.Date==date)&&(A.Employee.Name==EmpName))
        {
            Includes.Add(A => A.Employee);
            Includes.Add(A=>A.Employee.Department);
            //ThenIncludes.Add(A => A.Employee.Department);
        }
    }
}
