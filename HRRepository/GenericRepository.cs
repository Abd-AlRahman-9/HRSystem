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
    public class GenericRepository<T> : IRepository<T> where T : BaseTables
    {
        private HRContext context;

        public GenericRepository(HRContext context) 
        {
            this.context = context;
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            // Does using Find "ASYNC" Here is right ? && ! null reference
            var entity = await context.Set<T>().FindAsync(id);
            entity!.Deleted = true;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync(); 
        }

        public async Task<T> GetByIdAsync(int id) => await context.Set<T>().FindAsync(id);
        private IQueryable<T> ApplySpecification (ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.BuildQuery(context.Set<T>(), specification);
        }
        public async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
