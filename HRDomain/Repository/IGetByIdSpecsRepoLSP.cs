using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using HRDomain.Specification;

namespace HRDomain.Repository
{
    public interface IGetByIdSpecsRepoLSP<T> where T : BaseTables
    {
        Task<T> GetByIdWithSpecificationAsync(ISpecification<T> specification);
    }
}
