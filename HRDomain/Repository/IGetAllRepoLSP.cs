using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Repository
{
    public interface IGetAllRepoLSP<T> where T : BaseTables
    {
        Task<IEnumerable<T>> GetAllAsync();
    }
}
