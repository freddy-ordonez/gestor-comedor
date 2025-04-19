using ComedorInfantil.Gestion.Domain.Entities.InKindDonation;
using ComedorInfantil.Gestion.Domain.Entities.Inventory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class InventoryConfiguration
    {
        public InventoryConfiguration(EntityTypeBuilder<InventoryEntity> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(x => x.InventoryId);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.ProductName).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.EntryDate).IsRequired();
            builder.Property(x => x.ExpiryDate).IsRequired();

            builder.HasOne(x => x.InKindDonation)
                .WithOne(x => x.Inventory)
                .HasForeignKey<InKindDonationEntity>(x => x.ProductId);
        }
    }
}
