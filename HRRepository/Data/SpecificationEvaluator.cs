using HRDomain.Entities;
using HRDomain.Specification;
using Microsoft.EntityFrameworkCore;

namespace HRRepository.Data
{
    public class SpecificationEvaluator<T> where T : BaseTable
    {
        public static IQueryable<T> BuildQuery(IQueryable<T> EntryPoint,ISpecification<T> Specifications)
        {
            var query = EntryPoint;

            if (Specifications.Criteria != null)
                query = query.Where(Specifications.Criteria);

            if(Specifications.IsPaginationEnabled)
                query = query.Skip(Specifications.Skip).Take(Specifications.Take);

            if (Specifications.OrderBy != null)
                query = query.OrderBy(Specifications.OrderBy);

            if (Specifications.OrderByDescending != null)
                query = query.OrderByDescending(Specifications.OrderByDescending);

            query = Specifications.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            // Add ThenInclude expressions
            //foreach (var thenIncludeExpression in Specifications.ThenIncludes)
            //{
            //    query = query.ThenInclude(thenIncludeExpression);
            //}
            return query;

        }
    }
}
