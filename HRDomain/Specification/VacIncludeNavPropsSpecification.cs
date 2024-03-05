using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
