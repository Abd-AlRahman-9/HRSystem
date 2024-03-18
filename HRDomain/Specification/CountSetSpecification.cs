using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class CountSetSpecification:GenericSpecification<Department>
    {
        public CountSetSpecification(GetAllDeptsParams P) : base
            (
                D => (D.Deleted == true) && (D.Name == null)
            )
        {        }
    }
}
