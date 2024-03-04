using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using HRDomain.Repository;
using HRDomain.Specification;
using HRRepository.Data;

namespace HRRepository
{
    public class EmpsRepo : GenericRepository<Employee>,IGetAllRepoLSP<Employee>
    {
        public EmpsRepo(HRContext context) : base(context)
        {
        }

        public Task<IEnumerable<Employee>> GetAllWithSpecificationsAsync(ISpecification<Employee> specification)
        {
            throw new NotImplementedException();
        }
    }
}
