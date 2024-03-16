using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using HRDomain.Specification;

namespace HRDomain.Repository
{
    public interface IRepository<T> where T : BaseTable
    {
        Task<int> GetCountAsync(ISpecification<T> specification);
        Task<IEnumerable<T>> GetAllWithSpecificationsAsync(ISpecification<T> specification);
        Task AddAsync(T entity);
        Task UpdateAsync(Expression<Func<T, bool>> predicate, string date, T entity);
        // ?????????????????????????????????????????????
        Task UpdateOneToOneAsync<TEntity>(T entity, Expression<Func<T, TEntity>> navigationProperty, TEntity relatedEntity) where TEntity : BaseTable;
        Task DeleteAsync(Expression<Func<T, bool>> predicate, string name);
        Task<T> GetSpecified(ISpecification<T> specification);
        //Task<T> GetByNameWithSpecificationAsync(ISpecification<T> specification);
        //Task<T> GetByDateWithSpecificationAsync(ISpecification<T> specification);
        //Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification);
    }
}
