using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using HRRepository.Data.Configuartions;
using Microsoft.EntityFrameworkCore;

namespace HRRepository.Data
{
    public class HRContext:DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeVacation> EmployeeVacations { get; set; }
        public DbSet<Vacation> Vacations { get; set; }

        public HRContext(DbContextOptions options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Search about the reflection and assembly

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //modelBuilder.ApplyConfiguration(new EmployeesConfiguration());
            //modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            //modelBuilder.ApplyConfiguration(new VacationConfiguration());
            //modelBuilder.ApplyConfiguration(new EmployeeVacationConfiguration());
        }
    }
}
