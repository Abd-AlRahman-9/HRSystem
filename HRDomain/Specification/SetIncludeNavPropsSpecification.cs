using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class SetIncludeNavPropsSpecification : GenericSpecification<Department>
    {
        public SetIncludeNavPropsSpecification(GetAllDeptsParams _params) : base
            (
                D =>
                (D.Deleted == true) && (D.Name==null)
            )
        {

            if (!string.IsNullOrEmpty(_params.sort))
            {
                switch (_params.sort)
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
        }
        public SetIncludeNavPropsSpecification(int id) : base(D => (D.Id == id) && (D.Deleted == true))
        {}
    }
}