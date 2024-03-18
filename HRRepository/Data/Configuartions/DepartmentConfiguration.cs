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
            builder.HasIndex(p => p.Name).IsUnique();


            builder.Property(P => P.WorkDays).HasColumnType("tinyint").HasMaxLength(2);
            builder.Property(P => P.DeductHour).HasColumnType("decimal(6,2)");
            builder.Property(P => P.BonusHour).HasColumnType("decimal(6,2)");
            builder.Property(P => P.ComingTime).HasColumnType("time");
            builder.Property(P => P.LeaveTime).HasColumnType("time");
            builder.HasOne(D => D.Manager).WithOne(D=>D.department).HasForeignKey<Department>(D => D.ManagerId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
