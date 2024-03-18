using HRDomain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRRepository.Data.Configuartions
{
    public class VacationConfiguration : IEntityTypeConfiguration<Vacation>
    {
        public void Configure(EntityTypeBuilder<Vacation> builder)
        {
            builder.Property(P=>P.Deleted).HasColumnType("bit");
            builder.Property(P => P.Holiday).HasColumnType("bit");
            builder.Property(P => P.Name).HasMaxLength(25);
            builder.Property(P => P.Date).HasColumnType("date");
        }
    }
}
