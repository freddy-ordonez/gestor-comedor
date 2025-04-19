using ComedorInfantil.Gestion.Domain.Entities.MoneyDonation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class MoneyDonationConfiguration
    {
        public MoneyDonationConfiguration(EntityTypeBuilder<MoneyDonationEntity> builder)
        {
            builder.ToTable("MoneyDonations");
            builder.HasKey(x => x.MoneyDonationId);
            builder.Property(x => x.Porpuse).IsRequired();
            builder.Property(x => x.Amount).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(x => x.DonorId).IsRequired();
            builder.Property(x => x.DonationDate).IsRequired();

            builder.HasOne(x => x.Donor)
                .WithMany(x => x.MoneyDonations)
                .HasForeignKey(x => x.DonorId);
        }
    }
}
