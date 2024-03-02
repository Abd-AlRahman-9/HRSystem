using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Entities
{
    public class BaseTables
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}

// We need to search about the best practicies of doing these things
// 1] the self relationship
// 2] making the nationality as an Enum ?
// 3] how the EF ORM deal with Enums ?