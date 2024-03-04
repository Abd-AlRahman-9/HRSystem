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
    public class DepartmentRepository : GenericRepository<Department>,IGetByIdRepoLSP<Department>,IGetAllSpecsRepoLSP<Department>
    {
        public DepartmentRepository(HRContext context) : base(context){}
        public async Task<Department> GetByIdAsync(int id) => await context.Departments.FindAsync(id);
        public async Task<IEnumerable<Department>> GetAllWithSpecificationsAsync(ISpecification<Department> specification) => 
            await ApplySpecification(specification).ToListAsync();
        private IQueryable<Department> ApplySpecification(ISpecification<Department> specification)
        {
            return SpecificationEvaluator<Department>.BuildQuery(context.Set<Department>(), specification);
        }

    }
}
