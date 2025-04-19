using ComedorInfantil.Gestion.Domain.Entities.AssignmentActitvity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class AssignmentActivityConfiguration
    {
        public AssignmentActivityConfiguration(EntityTypeBuilder<AssignmentActitvityEntity> builder)
        {
            builder.ToTable("AssignmentActivities");
            builder.HasKey(x => x.AssignmentId);
            builder.HasIndex(x => new {x.ActivityId, x.VolunteerId}).IsUnique();
            builder.Property(x => x.ActivityId).IsRequired();
            builder.Property(x => x.VolunteerId).IsRequired();
            builder.Property(x => x.AssignmentDate).IsRequired();

            builder.HasOne(x => x.Volunteer)
                .WithMany(x => x.Assigments)
                .HasForeignKey(x => x.VolunteerId);

            builder.HasOne(x => x.Activity)
                .WithMany(x => x.Assignments)
                .HasForeignKey(x => x.ActivityId);
        }
    }
}
