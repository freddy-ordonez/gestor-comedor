using ComedorInfantil.Gestion.Domain.Entities.InKindDonation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class InKindDonationConfiguration
    {
        public InKindDonationConfiguration(EntityTypeBuilder<InKindDonationEntity> builder) 
        {
            builder.ToTable("InKindDonations");
            builder.HasKey(x => x.InKindDonationId);
            builder.HasIndex(x => x.ProductId).IsUnique();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.DonationDate).IsRequired();
            builder.Property(x => x.DonorId).IsRequired();

            builder.HasOne(x => x.Inventory)
                .WithOne(x => x.InKindDonation)
                .HasForeignKey<InKindDonationEntity>(x => x.ProductId);

            builder.HasOne(x => x.Donor)
                .WithMany(x => x.InKindDonations)
                .HasForeignKey(x => x.DonorId);
        }
    }
}
