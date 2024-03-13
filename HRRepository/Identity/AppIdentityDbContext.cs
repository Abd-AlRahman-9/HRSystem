using HRDomain.Entities.Identity;
using HRRepository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRRepository.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options): base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasData(
               new AppUser()
               {
                   FullName = "Ashraf Nouh",
                   Email = "ashraf@pioneers-solutions.com",
                   UserName = "Ashraf2024",
                   PasswordHash = "3s7raf@nou7",
                   Id = Guid.NewGuid().ToString(),
               }
                ) ;
            base.OnModelCreating(builder);
        }
    }
}
