using HRDomain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRRepository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                   FullName = "Ashraf Nouh",
                    Email = "ashraf@pioneers-solutions.com",
                    UserName = "Ashraf2024",
                };
                await userManager.CreateAsync(user, "3s7raf@nou7");

            }
        }
    }
}
