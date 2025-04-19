using ComedorInfantil.Gestion.Domain.Entities.Donor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class DonorConfiguration
    {
        public DonorConfiguration(EntityTypeBuilder<DonorEntity> builder)
        {
            builder.ToTable("Donors");
            builder.HasKey(x => x.DonorId);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.DonorType).IsRequired();
            builder.Property(x => x.Phone).IsRequired();

            builder.HasMany(x => x.MoneyDonations)
                .WithOne(x => x.Donor)
                .HasForeignKey(x => x.DonorId);

            builder.HasMany(x => x.InKindDonations)
                .WithOne(x => x.Donor)
                .HasForeignKey(x => x.DonorId);
        }
    }
}
