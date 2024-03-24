using HRDomain.Entities;
using HRDomain.Specification.Params;

namespace HRDomain.Specification.PaginatioCount
{
    public class CountDeptSpecification : GenericSpecification<Department>
    {
        public CountDeptSpecification(GetAllDeptsParams _params)
            : base
            (
                D =>
                D.Deleted == false &&
                (string.IsNullOrEmpty(_params.Search) || D.Name.ToLower().Contains(_params.Search)) &&
                (!_params.MngId.HasValue || D.ManagerId == _params.MngId.Value)
            )
        { }
    }
}
