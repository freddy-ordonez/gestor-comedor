using AutoMapper;
using ComedorInfantil.Gestion.Application.DTOs.Activity;
using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;
using ComedorInfantil.Gestion.Application.DTOs.Audit;
using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;
using ComedorInfantil.Gestion.Application.DTOs.Donor;
using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;
using ComedorInfantil.Gestion.Application.DTOs.Inventory;
using ComedorInfantil.Gestion.Application.DTOs.Module;
using ComedorInfantil.Gestion.Application.DTOs.ModuleByUser;
using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using ComedorInfantil.Gestion.Application.DTOs.TypeIdentification;
using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Application.DTOs.Volunteer;
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

namespace ComedorInfantil.Gestion.Application.Configuration
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Activities Mapper
            CreateMap<ActivityEntity, ActivityDTO>().ReverseMap();
            CreateMap<ActivityEntity, CreateActivityDTO>().ReverseMap();
            CreateMap<ActivityEntity, UpdateActivityDTO>().ReverseMap();
            #endregion

            #region Assigment Mapper
            CreateMap<AssignmentActitvityEntity, AssigmentActivityDTO>().ReverseMap();
            CreateMap<AssignmentActitvityEntity, CreateAssigmentActivityDTO>().ReverseMap();
            CreateMap<AssignmentActitvityEntity, AssigmentForActivityDTO>().ReverseMap();
            CreateMap<AssignmentActitvityEntity, AssigmentForVolunteerDTO>().ReverseMap();
            #endregion

            #region Audit Mapper
            CreateMap<AuditEntity, AuditDTO>().ReverseMap();
            CreateMap<AuditEntity, CreateAuditDTO>().ReverseMap();
            #endregion

            #region Beneficiary Mapper
            CreateMap<BeneficiaryEntity, CreateBeneficiaryDTO>().ReverseMap();
            CreateMap<BeneficiaryEntity, BeneficiaryDTO>().ReverseMap();
            CreateMap<BeneficiaryEntity, UpdateBeneficiaryDTO>().ReverseMap();
            #endregion

            #region Donor Mapper
            CreateMap<DonorEntity, DonorDTO>().ReverseMap();
            CreateMap<DonorEntity, CreateDonorDTO>().ReverseMap();
            CreateMap<DonorEntity, UpdateDonorDTO>().ReverseMap();
            #endregion

            #region InKindDonation Mapper
            CreateMap<InKindDonationEntity, CreateInKindDonationDTO>().ReverseMap();
            CreateMap<InKindDonationEntity, InKindDonationDTO>().ReverseMap();
            #endregion

            #region Inventory Mapper
            CreateMap<InventoryEntity, CreateInventoryDTO>().ReverseMap();
            CreateMap<InventoryEntity, UpdateInventoryDTO>().ReverseMap();
            CreateMap<InventoryEntity, InventoryDTO>().ReverseMap();
            #endregion

            #region ModuleByUser Mapper
            CreateMap<ModuleForUserEntity, CreateModuleByUserDTO>().ReverseMap();
            CreateMap<ModuleForUserEntity, ModuleByUserDTO>().ReverseMap();
            #endregion

            #region MoneyDonation Mapper
            CreateMap<MoneyDonationEntity, CreateMoneyDonationDTO>().ReverseMap();
            CreateMap<MoneyDonationEntity, UpdateMoneyDonationDTO>().ReverseMap();
            CreateMap<MoneyDonationEntity, MoneyDonationDTO>().ReverseMap();
            #endregion

            #region Module Mapper
            CreateMap<ModuleEntity, ModuleDTO>().ReverseMap();
            #endregion

            #region TypeIdentification Mapper
            CreateMap<TypeIdentificationEntity, TypeIdentificationDTO>().ReverseMap();
            #endregion

            #region User Mapper
            CreateMap<UserEntity, CreateUserDTO>().ReverseMap();
            CreateMap<UserEntity, UpdateUserDTO>().ReverseMap();
            CreateMap<UserEntity, UserDTO>().ReverseMap();
            CreateMap<UserEntity, AuthUserLogin>().ReverseMap();
            CreateMap<UserEntity, UpdatePasswordUserDTO>().ReverseMap();
            #endregion

            #region Volunteer Mapper
            CreateMap<VolunteerEntity, VolunteerDTO>().ReverseMap();
            CreateMap<VolunteerEntity, CreateVolunteerDTO>().ReverseMap();
            CreateMap<VolunteerEntity, UpdateVolunteerDTO>().ReverseMap();
            #endregion

        }
    }
}
