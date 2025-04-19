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
using Microsoft.EntityFrameworkCore;

namespace ComedorInfantil.Gestion.Application.DataBase
{
    public interface IDataBaseService
    {
        DbSet<ActivityEntity> Activities { get; set; }
        DbSet<AssignmentActitvityEntity> AssignmentActitvities { get; set; }
        DbSet<AuditEntity> Audits { get; set; }
        DbSet<BeneficiaryEntity> Beneficiaries { get; set; }
        DbSet<DonorEntity> Donors { get; set; }
        DbSet<InKindDonationEntity> InKindDonations { get; set; }
        DbSet<InventoryEntity> Inventories { get; set; }
        DbSet<MoneyDonationEntity> MoneyDonations { get; set; }
        DbSet<TypeIdentificationEntity> TypeIdentifications { get; set; }
        DbSet<UserEntity> Users { get; set; }
        DbSet<VolunteerEntity> Volunteers { get; set; }
        DbSet<ModuleEntity> Modules { get; set; }
        DbSet<ModuleForUserEntity> ModuleForUsers { get; set; }
        Task<bool> SaveAsync();
    }
}
