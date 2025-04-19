using ComedorInfantil.Gestion.Domain.Entities.ModuleForUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComedorInfantil.Gestion.Persistence.Configuration
{
    public class ModuleForUserEntityConfigurarion
    {
        public ModuleForUserEntityConfigurarion(EntityTypeBuilder<ModuleForUserEntity> builder)
        {
            builder.ToTable("ModulesForUser");
            builder.HasKey(x => x.ModuleForUserId);
            builder.HasIndex(x => new {x.ModuleId, x.UserId}).IsUnique();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ModuleId).IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.ModuleForUserEntities)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Module)
                .WithMany(x => x.ModuleForUserEntities)
                .HasForeignKey(x => x.ModuleId);
        }
    }
}
