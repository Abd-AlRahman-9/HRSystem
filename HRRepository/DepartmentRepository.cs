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
    public class DepartmentRepository : GenericRepository<Department>,IGetByIdRepoLSP<Department>,IGetAllSpecsRepoLSP<Employee>,IGetAllRepoLSP<Department>
    {
        public DepartmentRepository(HRContext context) : base(context){}
        public async Task<Department> GetByIdAsync(int id) => await context.Departments.FindAsync(id);
        public async Task<IEnumerable<Employee>> GetAllWithSpecificationsAsync(ISpecification<Employee> specification) => 
            await ApplySpecification(specification).ToListAsync();
        private IQueryable<Employee> ApplySpecification(ISpecification<Employee> specification)
        {
            return SpecificationEvaluator<Employee>.BuildQuery(context.Set<Employee>(), specification);
        }

        public async Task<IEnumerable<Department>> GetAllAsync() => await context.Departments.Include(D => D.Manager.Name).ToListAsync();
    }
}
