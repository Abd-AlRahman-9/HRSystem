using Microsoft.AspNetCore.Identity;

namespace HRDomain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
