using ComedorInfantil.Gestion.Domain.Entities.Beneficiary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class BeneficiaryConfiguration
    {
        public BeneficiaryConfiguration(EntityTypeBuilder<BeneficiaryEntity> builder)
        {
            builder.ToTable("Beneficiaries");
            builder.HasKey(x => x.BeneficiaryId);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
        }
    }
}
