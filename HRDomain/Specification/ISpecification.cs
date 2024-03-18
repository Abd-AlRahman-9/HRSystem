using System.Linq.Expressions;
using HRDomain.Entities;

namespace HRDomain.Specification
{
    public interface ISpecification<T> where T : BaseTable
    {
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; }
        public List<Expression<Func<T,object>>> ThenIncludes { get; set; }
        public Expression<Func<T,object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
