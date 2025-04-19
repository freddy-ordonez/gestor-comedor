using ComedorInfantil.Gestion.Domain.Entities.Activity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class ActivityConfiguration
    {
        public ActivityConfiguration(EntityTypeBuilder<ActivityEntity> builder)
        {
            builder.ToTable("Activities");
            builder.HasKey(x => x.ActivityId);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();

            builder.HasMany(x => x.Assignments)
                .WithOne(x => x.Activity)
                .HasForeignKey(x => x.ActivityId);
        }
    }
}
