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
        //public async Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification)
        //{
        //    return await ApplySpecification(specification).FirstOrDefaultAsync();
        //}
        //public async Task<IEnumerable<T>> GetAllWithSpecificationsAsync(ISpecification<T> specification) => await ApplySpecification(specification).ToListAsync();

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
