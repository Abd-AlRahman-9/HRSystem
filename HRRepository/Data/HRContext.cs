using System.Reflection;
using HRDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRRepository.Data
{
    public class HRContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeVacation> EmployeeVacations { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<EmployeeAttendace> EmployeeAttendaces { get; set; }
        public HRContext() { }

        public HRContext(DbContextOptions<HRContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Configure the value converter for DateOnly properties
            //var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            //    v => v.ToDateTime(TimeOnly.MinValue),
            //    v => DateOnly.FromDateTime(v));

            //// Configure the value converter for TimeSpan properties
            //var timeSpanConverter = new ValueConverter<TimeSpan, long>(
            //    v => v.Ticks,
            //    v => TimeSpan.FromTicks(v));

            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.BirthDate)
            //    .HasConversion(dateOnlyConverter);

            //modelBuilder.Entity<EmployeeAttendace>()
            //    .Property(e => e.Date)
            //    .HasConversion(dateOnlyConverter);

            //modelBuilder.Entity<Vacation>()
            //   .Property(e => e.Date)
            //   .HasConversion(dateOnlyConverter);

            ///////////////////////////////////////////
            //modelBuilder.Entity<Department>()
            //    .Property(e => e.ComingTime)
            //    .HasConversion(timeSpanConverter);
            //modelBuilder.Entity<Department>()
            //   .Property(e => e.LeaveTime)
            //   .HasConversion(timeSpanConverter);
            //base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<EmployeeAttendace>()
            //  .Property(e => e.Attendance)
            //  .HasConversion(timeSpanConverter);
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<EmployeeAttendace>()
            //.Property(e => e.Leave)
            //.HasConversion(timeSpanConverter);
            //base.OnModelCreating(modelBuilder);
            #endregion

            // Search about the reflection and assembly

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

