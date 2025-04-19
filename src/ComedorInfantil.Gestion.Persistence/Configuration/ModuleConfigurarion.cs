using ComedorInfantil.Gestion.Domain.Entities.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class ModuleConfigurarion
    {
        public ModuleConfigurarion(EntityTypeBuilder<ModuleEntity> builder)
        {
            builder.ToTable("Modules");
            builder.HasKey(x => x.ModuleId);
            builder.Property(x => x.ModuleName).IsRequired();
            builder.Property(x => x.ClassCSS).IsRequired();
            builder.Property(x => x.Link).IsRequired();

            builder.HasMany(x => x.ModuleForUserEntities)
                .WithOne(x => x.Module)
                .HasForeignKey(x => x.ModuleId);
        }
    }
}
