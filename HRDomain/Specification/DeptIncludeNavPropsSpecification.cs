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
        public DeptIncludeNavPropsSpecification(GetAllDeptsParams _params) : base
            (
                D =>(!_params.MngId.HasValue || D.ManagerId == _params.MngId.Value)
            )
        {
            Includes.Add(E => E.Manager);
            //Includes.Add(E => E.Employees);
            ApplyPagination(_params.PageSize.Value * (_params.PageCount.Value - 1), _params.PageSize.Value);

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
        public DeptIncludeNavPropsSpecification(int id):base(E=>E.Id==id)
        {
            Includes.Add(E => E.Manager);
            //Includes.Add(E => E.Employees);
        }
    }
}
