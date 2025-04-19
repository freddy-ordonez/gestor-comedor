using ComedorInfantil.Gestion.Application.DataBase;
using ComedorInfantil.Gestion.Domain.Entities.Activity;
using ComedorInfantil.Gestion.Domain.Entities.AssignmentActitvity;
using ComedorInfantil.Gestion.Domain.Entities.Audit;
using ComedorInfantil.Gestion.Domain.Entities.Beneficiary;
using ComedorInfantil.Gestion.Domain.Entities.Donor;
using ComedorInfantil.Gestion.Domain.Entities.InKindDonation;
using ComedorInfantil.Gestion.Domain.Entities.Inventory;
using ComedorInfantil.Gestion.Domain.Entities.Module;
using ComedorInfantil.Gestion.Domain.Entities.ModuleForUser;
using ComedorInfantil.Gestion.Domain.Entities.MoneyDonation;
using ComedorInfantil.Gestion.Domain.Entities.TypeIdentification;
using ComedorInfantil.Gestion.Domain.Entities.User;
using ComedorInfantil.Gestion.Domain.Entities.Volunteer;
using ComedorInfantil.Gestion.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Persistence.DataBase
{
    public class DataBaseService : DbContext, IDataBaseService
    {
        public DataBaseService(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ActivityEntity> Activities { get; set; }
        public DbSet<AssignmentActitvityEntity> AssignmentActitvities { get; set; }
        public DbSet<AuditEntity> Audits { get; set; }
        public DbSet<BeneficiaryEntity> Beneficiaries { get; set; }
        public DbSet<DonorEntity> Donors { get; set; }
        public DbSet<InKindDonationEntity> InKindDonations { get; set; }
        public DbSet<InventoryEntity> Inventories { get; set; }
        public DbSet<MoneyDonationEntity> MoneyDonations { get; set; }
        public DbSet<TypeIdentificationEntity> TypeIdentifications { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<VolunteerEntity> Volunteers { get; set; }
        public DbSet<ModuleEntity> Modules { get; set; }
        public DbSet<ModuleForUserEntity> ModuleForUsers {  get; set; } 

        public async Task<bool> SaveAsync()
        {
            return await SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EntityConfiguration(modelBuilder);
        }

        private void EntityConfiguration(ModelBuilder modelBuilder)
        {
            new ActivityConfiguration(modelBuilder.Entity<ActivityEntity>());
            new AssignmentActivityConfiguration(modelBuilder.Entity<AssignmentActitvityEntity>());
            new AuditConfiguration(modelBuilder.Entity<AuditEntity>());
            new BeneficiaryConfiguration(modelBuilder.Entity<BeneficiaryEntity>());
            new DonorConfiguration(modelBuilder.Entity<DonorEntity>());
            new InKindDonationConfiguration(modelBuilder.Entity<InKindDonationEntity>());
            new InventoryConfiguration(modelBuilder.Entity<InventoryEntity>());
            new MoneyDonationConfiguration(modelBuilder.Entity<MoneyDonationEntity>());
            new TypeIdentificationConfiguration(modelBuilder.Entity<TypeIdentificationEntity>());
            new UserConfiguration(modelBuilder.Entity<UserEntity>());
            new VolunteerConfiguration(modelBuilder.Entity<VolunteerEntity>());
            new ModuleConfigurarion(modelBuilder.Entity<ModuleEntity>());
            new ModuleForUserEntityConfigurarion(modelBuilder.Entity<ModuleForUserEntity>());
        }
    }
}
