using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class VacIncludeNavPropsSpecification:GenericSpecification<Vacation>
    {
        public VacIncludeNavPropsSpecification(DateOnly date) :base(V=>(V.Date == date)&&(V.Deleted==false)&&(V.Holiday==true))
        { 
        }
    }
}
