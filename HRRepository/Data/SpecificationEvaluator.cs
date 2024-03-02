using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using HRDomain.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace HRRepository.Data
{
    public class SpecificationEvaluator<T> where T : BaseTables
    {
        public static IQueryable<T> BuildQuery(IQueryable<T> EntryPoint,ISpecification<T> Specifications)
        {
            var query = EntryPoint;

            if (Specifications.Criteria != null)
                query = query.Where(Specifications.Criteria);

            query = Specifications.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));

            return query;

        }
    }
}
