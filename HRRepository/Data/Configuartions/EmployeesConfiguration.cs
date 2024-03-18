using HRDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRRepository.Data.Configuartions
{
    public class EmployeesConfiguration : IEntityTypeConfiguration<Employee>
    {
        //// Example: Get all employees and their managers
        //var employeesWithManagers = dbContext.Employees
        //    .Include(e => e.Manager)
        //    .ToList();
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(P=>P.Deleted).HasColumnType("bit");
            builder.Property(P=>P.NationalID).HasColumnType("char(14)");
            builder.Property(P => P.PhoneNumber).HasMaxLength(25);
            builder.Property(P => P.Name).HasMaxLength(50);
            builder.Property(P => P.HireData).HasColumnType("date");
            builder.Property(P => P.BirthDate).HasColumnType("date");
            builder.Property(P => P.Gender).HasMaxLength(9);
            builder.Property(P=>P.VacationsRecord).HasColumnType("tinyint").HasMaxLength(2);
            builder.Property(P => P.Address).HasMaxLength(100);
            // Employee has one Manager
            // Manager can have many Employees
            // Foreign key property
            builder.HasOne(e => e.manager).WithMany(e=>e.employees).HasForeignKey(e => e.ManagerId).OnDelete(DeleteBehavior.NoAction); // Optional: Set delete behavior
            builder.HasOne(e => e.Department).WithMany(d=>d.Employees).HasForeignKey(e=> e.DeptId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
