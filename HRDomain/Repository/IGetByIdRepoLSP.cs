using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;

namespace HRDomain.Repository
{
    public interface IGetByIdRepoLSP<T> where T : BaseTables
    {
        public Task<T> GetByIdAsync(int id);
    }
}
