using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification);
        Task AddAsync(T entity);
        Task UpdateAsync(int id,T entity);
        Task DeleteAsync(int id);
        Task<T> GetByNameWithSpecificationAsync(ISpecification<T> specification);
    }
}
