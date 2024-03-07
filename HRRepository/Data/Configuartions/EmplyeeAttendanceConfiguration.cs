using HRDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRRepository.Data.Configuartions
{
    public class EmplyeeAttendanceConfiguration : IEntityTypeConfiguration<EmployeeAttendace>
    {
        public void Configure(EntityTypeBuilder<EmployeeAttendace> builder)
        {
            builder.Property(e => e.Attendance).HasColumnType("time").IsRequired();
            builder.Property(e => e.Leave).HasColumnType("time").IsRequired();
            builder.Property(e => e.Date).HasColumnType("date").IsRequired();
            builder.HasOne(e => e.Employee).WithMany(s=>s.employeeAttendaces).HasForeignKey(e => e.EmployeeId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
