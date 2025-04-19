using ComedorInfantil.Gestion.Domain.Entities.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class AuditConfiguration
    {
        public AuditConfiguration(EntityTypeBuilder<AuditEntity> builder)
        {
            builder.ToTable("Audits");
            builder.HasKey(x => x.AuditId);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Action).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.ActionDate).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Audits)
                .HasForeignKey(x => x.UserId);
        }
    }
}
