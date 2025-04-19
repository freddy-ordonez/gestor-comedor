using ComedorInfantil.Gestion.Domain.Entities.TypeIdentification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class TypeIdentificationConfiguration
    {
        public TypeIdentificationConfiguration(EntityTypeBuilder<TypeIdentificationEntity> builder)
        {
            builder.ToTable("TypeIdentifications");
            builder.HasKey(x => x.TypeIdentificationId);
            builder.Property(x => x.TypeIdentification).IsRequired();
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
