using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HRDomain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
