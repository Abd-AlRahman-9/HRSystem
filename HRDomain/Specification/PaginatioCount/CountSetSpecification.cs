using HRDomain.Entities;
using HRDomain.Specification.Params;

namespace HRDomain.Specification.PaginatioCount
{
    public class CountSetSpecification : GenericSpecification<Department>
    {
        public CountSetSpecification(GetAllDeptsParams P) : base
            (
                D => D.Deleted == true && D.Name == null
            )
        { }
    }
}
