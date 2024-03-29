﻿using HRDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRRepository.Data.Configuartions
{
    public class EmployeeVacationConfiguration : IEntityTypeConfiguration<EmployeeVacation>
    {
        public void Configure(EntityTypeBuilder<EmployeeVacation> builder)
        {
            builder.Property(EmpVac=>EmpVac.Deleted).HasColumnType("bit");
            //builder.HasKey(EmpVac => new { EmpVac.Id, EmpVac.VacationId });
            builder.HasOne(EmpVac => EmpVac.Employee).WithMany(s=>s.EmployeeVacations).HasForeignKey(EmpVac => EmpVac.EmployeeId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(EmpVac => EmpVac.Vacation).WithMany(s=>s.EmployeesVacation).HasForeignKey(EmpVac => EmpVac.VacationId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
