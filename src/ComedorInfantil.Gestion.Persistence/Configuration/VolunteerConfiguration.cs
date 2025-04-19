using ComedorInfantil.Gestion.Domain.Entities.TypeIdentification;
using ComedorInfantil.Gestion.Domain.Entities.Volunteer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class VolunteerConfiguration
    {
        public VolunteerConfiguration(EntityTypeBuilder<VolunteerEntity> builder)
        {
            builder.ToTable("Volunteers");
            builder.HasKey(x => x.VolunteerId);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Availability).IsRequired();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Identification).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.TypeIdentification).IsRequired();

            builder.HasMany(x => x.Assigments)
                .WithOne(x => x.Volunteer)
                .HasForeignKey(x => x.VolunteerId);

            builder.HasOne(x => x.TypeIdentificationEntity)
                .WithOne(x => x.Volunteer)
                .HasForeignKey<VolunteerEntity>(x => x.TypeIdentification);
        }
    }
}
