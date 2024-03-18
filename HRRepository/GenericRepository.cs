using System.Linq.Expressions;
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
        public async Task DeleteAsync(Expression<Func<T, bool>> predicate, string name)
        {
            // Does using Find "ASYNC" Here is right ? && ! null reference
            var entity = await context.Set<T>().SingleOrDefaultAsync(predicate);
            entity!.Deleted = true;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync(); 
        }
        
        public async Task UpdateAsync(Expression<Func<T, bool>> predicate, string name, T entity)
        {
            var entityToEdit = await context.Set<T>().SingleOrDefaultAsync(predicate);
           
            if (entityToEdit != null)
            {
                // Update each property individually
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    if (property.Name != "Id")
                    {
                        var newValue = property.GetValue(entity);
                        if (newValue != null)
                        {
                            property.SetValue(entityToEdit, newValue);
                        }
                    }

                }
                //context.Entry(entityToEdit).CurrentValues.SetValues(entity);

                context.Set<T>().Update(entityToEdit);

                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateOneToOneAsync<TEntity>(T entity, Expression<Func<T, TEntity>> navigationProperty, TEntity relatedEntity) where TEntity : BaseTable
        {
            context.Entry(entity).Reference(navigationProperty).IsModified = true;
            context.Entry(entity).Property(navigationProperty).CurrentValue = relatedEntity;
            await context.SaveChangesAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }
        public async Task<T> GetSpecified(ISpecification<T> specification) => await ApplySpecification(specification).FirstOrDefaultAsync();

    }
}
