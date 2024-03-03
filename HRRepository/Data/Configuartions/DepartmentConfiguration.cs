using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRRepository.Data.Configuartions
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(P => P.Deleted).HasColumnType("bit");
            builder.Property(P => P.Name).HasMaxLength(25);
            builder.Property(P => P.WorkDays).HasColumnType("tinyint").HasMaxLength(2);
            builder.Property(P => P.DeductHour).HasColumnType("decimal(18,2)");
            builder.Property(P => P.BonusHour).HasColumnType("decimal(18,2)");
            builder.Property(P => P.ComingTime).HasColumnType("time");
            builder.Property(P => P.LeaveTime).HasColumnType("time");
            builder.HasOne(D => D.Manager).WithOne().HasForeignKey<Department>(D => D.ManagerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
