using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using HRDomain.Repository;
using HRDomain.Specification;
using HRRepository.Data;
using Microsoft.EntityFrameworkCore;

namespace HRRepository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseTable
    {
        protected HRContext context;

        public GenericRepository(HRContext context) 
        {
            this.context = context;
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.BuildQuery(context.Set<T>(), specification);
        }
        public async Task<T> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAllWithSpecificationsAsync(ISpecification<T> specification) =>
            await ApplySpecification(specification).ToListAsync();
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(string name)
        {
            // Does using Find "ASYNC" Here is right ? && ! null reference
            var entity = await context.Set<T>().FindAsync(name);
            entity!.Deleted = true;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync(); 
        }
        
        public async Task UpdateAsync(string name, T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }
        public async Task<T> GetSpecified(ISpecification<T> specification) => await ApplySpecification(specification).FirstOrDefaultAsync();

        //public async Task<T> GetByNameAndDateWithSpecificationAsync (ISpecification<T> specification) {
        //    return await ApplySpecification(specification).FirstOrDefaultAsync();
        //}
        //public async Task<T> GetByNameWithSpecificationAsync(ISpecification<T> specification)
        //{
        //    return await ApplySpecification(specification).FirstOrDefaultAsync();
        //}
        //public async Task<T> GetByDateWithSpecificationAsync(ISpecification<T> specification)
        //{
        //    return await ApplySpecification(specification).FirstOrDefaultAsync();
        //}
        //public async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification)
        //{
        //    return await ApplySpecification(specification).FirstOrDefaultAsync();
        //}
    }
}
