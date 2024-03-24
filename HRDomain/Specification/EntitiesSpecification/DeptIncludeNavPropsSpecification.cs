using HRDomain.Entities;
using HRDomain.Specification.Params;

namespace HRDomain.Specification.EntitiesSpecification
{
    public class DeptIncludeNavPropsSpecification : GenericSpecification<Department>
    {
        public DeptIncludeNavPropsSpecification(GetAllDeptsParams _params) : base
            (
                D =>
                D.Deleted == false &&
                (string.IsNullOrEmpty(_params.Search) || D.Name.ToLower().Contains(_params.Search)) &&
                (!_params.MngId.HasValue || D.ManagerId == _params.MngId.Value)
            )
        {
            Includes.Add(E => E.Manager);

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
        public DeptIncludeNavPropsSpecification(string name) : base(D => D.Name == name && D.Deleted == false)
        {
            Includes.Add(E => E.Manager);
        }
    }
}
