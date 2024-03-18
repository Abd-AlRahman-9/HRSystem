using HRDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace HRDomain.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, UserManager<AppUser> userManager);
    }
}
