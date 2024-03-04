using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using HRDomain.Specification;

namespace HRDomain.Repository
{
    public interface IGetAllRepoLSP<T> where T : BaseTable
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
