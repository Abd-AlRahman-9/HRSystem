using HRDomain.Entities;

namespace HRDomain.Specification.EntitiesSpecification
{
    public class VacIncludeNavPropsSpecification : GenericSpecification<Vacation>
    {
        public VacIncludeNavPropsSpecification() : base(V => V.Deleted == false && V.Holiday == true) { }
        public VacIncludeNavPropsSpecification(DateOnly date) : base(V => V.Date == date && V.Deleted == false && V.Holiday == true)
        {
        }

    }
}
