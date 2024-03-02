using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public class GenericSpecification<T> : ISpecification<T> where T : BaseTables
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>> ();
        public GenericSpecification(){}
        public GenericSpecification(Expression<Func<T,bool>> criteria) { this.Criteria = criteria; }
    }
}
