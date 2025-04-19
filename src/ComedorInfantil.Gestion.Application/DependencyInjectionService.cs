using AutoMapper;
using ComedorInfantil.Gestion.Application.Configuration;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Command.CreateActivity;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Command.DeleteActitvity;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Command.UpdateActivity;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetActivityByIdQuerie;
using ComedorInfantil.Gestion.Application.DataBase.Activity.Queries.GetAllActivity;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.CreateAssigmentActivity;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Command.DeleteAssignmentActivity;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForActivity;
using ComedorInfantil.Gestion.Application.DataBase.AssigmentActivity.Queries.GetAssignmentForVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Command.CreateAudit;
using ComedorInfantil.Gestion.Application.DataBase.Audit.Queries.GetAllAudit;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.CreateBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.DeleteBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Command.UpdateBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetAllBeneficiary;
using ComedorInfantil.Gestion.Application.DataBase.Beneficiary.Queries.GetBeneficiaryById;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Command.CreateDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Command.DeleteDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Command.UpdateDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetAllDonor;
using ComedorInfantil.Gestion.Application.DataBase.Donor.Queries.GetDonorById;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.CreateInKindDonationCommand;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Command.DeleteInKindDonation;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetAllInKindDonation;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetInKindDonationForOneDonor;
using ComedorInfantil.Gestion.Application.DataBase.InKindDonation.Queries.GetInKindDonationForOneInventary;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.CreateInventory;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.DeleteInventory;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Command.UpdateInventory;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetAllInventoy;
using ComedorInfantil.Gestion.Application.DataBase.Inventory.Queries.GetInventoryById;
using ComedorInfantil.Gestion.Application.DataBase.Module.Queries.GetAllModule;
using ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.CreateModuleByUser;
using ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Command.DeleteModuleByUser;
using ComedorInfantil.Gestion.Application.DataBase.ModuleByUser.Queries.GetModuleByUserId;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.CreateMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.DeleteMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Command.UpdateMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonation;
using ComedorInfantil.Gestion.Application.DataBase.MoneyDonation.Queries.GetAllMoneyDonationByDonor;
using ComedorInfantil.Gestion.Application.DataBase.TypeIdentification.Queries.GetAllTypeIdentification;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.CreateUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.DeleteUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdatePasswordUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Command.UpdateUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetAllUser;
using ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserByEmailAndPassword;
using ComedorInfantil.Gestion.Application.DataBase.User.Queries.GetUserById;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.CreateVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.DeleteVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Command.UpdateVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetAllVolunteer;
using ComedorInfantil.Gestion.Application.DataBase.Volunteer.Queries.GetVolunteerById;
using ComedorInfantil.Gestion.Application.Interfaces;
using ComedorInfantil.Gestion.Application.Services;
using ComedorInfantil.Gestion.Application.Validators.Activity;
using ComedorInfantil.Gestion.Application.Validators.AssigmentAcivity;
using ComedorInfantil.Gestion.Application.Validators.Beneficiary;
using ComedorInfantil.Gestion.Application.Validators.Donor;
using ComedorInfantil.Gestion.Application.Validators.InKindDonation;
using ComedorInfantil.Gestion.Application.Validators.Inventory;
using ComedorInfantil.Gestion.Application.Validators.ModuleByUser;
using ComedorInfantil.Gestion.Application.Validators.MoneyDonation;
using ComedorInfantil.Gestion.Application.Validators.User;
using ComedorInfantil.Gestion.Application.Validators.Volunteer;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ComedorInfantil.Gestion.Application
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IPasswordService, PasswordService>();
            #endregion

            #region Services AutoMapper
            var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapper.CreateMapper());
            #endregion

            #region Services DataBase

                #region Services Database Activity
                services.AddTransient<ICreateActivityCommand, CreateActivityCommand>();
                services.AddTransient<IUpdateActivityCommand, UpdateActivityCommand>();
                services.AddTransient<IDeleteActivityCommand, DeleteActivityCommand>();
                services.AddTransient<IGetAllActivityQuerie, GetAllActivityQuerie>();
                services.AddTransient<IGetActivityByIdQuerie, GetActivityByIdQuerie>();
                #endregion

                #region Services Database AssigmentActivity
                services.AddTransient<ICreateAssigmentActivityCommand, CreateAssigmentActivityCommand>();
                services.AddTransient<IDeleteAssignmentActivityCommand, DeleteAssignmentActivityCommand>();
                services.AddTransient<IGetAssignmentForActivityQuerie, GetAssignmentForActivityQuerie>();
                services.AddTransient<IGetAssignmentForVolunteerQuerie, GetAssignmentForVolunteerQuerie>();
                #endregion

                #region Services Database Audit
                services.AddTransient<ICreateAuditCommand, CreateAuiditCommand>();
                services.AddTransient<IGetAllAuditQuerie, GetAllAuditQuerie>();
                #endregion

                #region Services Database Beneficiary
                services.AddTransient<ICreateBeneficiaryCommand, CreateBeneficiaryCommand>();
                services.AddTransient<IUpdateBeneficiaryCommand, UpdateBeneficiaryCommand>();
                services.AddTransient<IDeleteBeneficiaryCommand, DeleteBeneficiaryCommand>();
                services.AddTransient<IGetAllBeneficiaryQuerie, GetAllBeneficiaryQuerie>();
                services.AddTransient<IGetBeneficiaryByIdQuerie, GetBeneficiaryByIdQuerie>();
                #endregion

                #region Services Database Donor
                services.AddTransient<ICreateDonorCommand, CreateDonorCommand>();
                services.AddTransient<IUpdateDonorCommand, UpdateDonorCommand>();
                services.AddTransient<IDeleteDonorCommand, DeleteDonorCommand>();
                services.AddTransient<IGetAllDonorQuerie, GetAllDonorQuerie>();
                services.AddTransient<IGetDonorByIdQuerie, GetDonorByIdQuerie>();
                #endregion

                #region Services Database InKindDonation
                services.AddTransient<ICreateInKindDonationCommand, CreateInKindDonationCommand>();
                services.AddTransient<IDeleteInKindDonationCommand, DeleteInKindDonationCommand>();
                services.AddTransient<IGetInKindDonationForOneInventaryQuerie, GetInKindDonationForOneInventaryQuerie>();
                services.AddTransient<IGetAllInKindDonationForOneDonorQuerie, GetAllInKindDonationForOneDonorQuerie>();
                services.AddTransient<IGetAllInKindDonationQuerie, GetAllInKindDonationQuerie>();
                #endregion

                #region Services Database Inventory
                services.AddTransient<ICreateInventoryCommand, CreateInventoryCommand>();
                services.AddTransient<IUpdateInventoryCommand, UpdateInventoryCommand>();
                services.AddTransient<IDeleteInventoryCommand, DeleteInventoryCommand>();
                services.AddTransient<IGetAllInventoyQuerie, GetAllInventoyQuerie>();
                services.AddTransient<IGetInventoryByIdQuerie, GetInventoryByIdQuerie>();
                #endregion

                #region Services Database Module
                services.AddTransient<IGetAllModuleQuerie, GetAllModuleQuerie>();
                #endregion

                #region Services Database ModuleByUsuario
                services.AddTransient<ICreateModuleByUserCommand, CreateModuleByUserCommand>();
                services.AddTransient<IDeleteModuleByUserCommand, DeleteModuleByUserCommand>();
                services.AddTransient<IGetModuleByUserIdQuerie, GetModuleByUserIdQuerie>();
                #endregion

                #region Services Database MoneyDonation
                services.AddTransient<ICreateMoneyDonationCommand, CreateMoneyDonationCommand>();
                services.AddTransient<IUpdateMoneyDonationCommand, UpdateMoneyDonationCommand>();
                services.AddTransient<IDeleteMoneyDonationCommand, DeleteMoneyDonationCommand>();
                services.AddTransient<IGetAllMoneyDonationByDonorQuerie, GetAllMoneyDonationByDonorQuerie>();
                services.AddTransient<IGetAllMoneyDonationQuerie, GetAllMoneyDonationQuerie>();
                #endregion

                #region Services Database TypeIdentification
                services.AddTransient<IGetAllTypeIdentificationQuerie, GetAllTypeIdentificationQuerie>();
                #endregion

                #region Services Database User
                services.AddTransient<ICreateUserCommand, CreateUserCommand>();
                services.AddTransient<IUpdatePasswordUserCommand, UpdatePasswordUserCommand>();
                services.AddTransient<IUpdateUserCommand, UpdateUserCommand>();
                services.AddTransient<IDeleteUserCommand, DeleteUserCommand>();
                services.AddTransient<IGetAllUserQuerie, GetAllUserQuerie>();
                services.AddTransient<IGetUserByIdQuerie, GetUserByIdQuerie>();
                services.AddTransient<IGetUserByEmailAndPasswordQuerie, GetUserByEmailAndPasswordQuerie>();
                #endregion

                #region Sevices Database Volunteer
                services.AddTransient<ICreateVolunteerCommand, CreateVolunteerCommand>();
                services.AddTransient<IUpdateVolunteerCommand, UpdateVolunteerCommand>();
                services.AddTransient<IDeleteVolunteerCommand, DeleteVolunteerCommand>();
                services.AddTransient<IGetAllVolunteerQuerie, GetAllVolunteerQuerie>();
                services.AddTransient<IGetVolunteerByIdQuerie, GetVolunteerByIdQuerie>();
                #endregion

            #endregion

            #region Services Validator

            #region Services ActivityValidator
            services.AddValidatorsFromAssemblyContaining<CreateActivityValidator>();
                services.AddValidatorsFromAssemblyContaining<UpdateActivityValidator>();
                #endregion

                #region Services AssignmetActivityValidator
                services.AddValidatorsFromAssemblyContaining<CreateAssignmentActivityValidator>();
                #endregion

                #region Services BeneficiaryValidator
                services.AddValidatorsFromAssemblyContaining<CreateBeneficiaryValidator>();
                services.AddValidatorsFromAssemblyContaining<UpdateBeneficiaryValidator>();
                #endregion

                #region Services DonorValidator
                services.AddValidatorsFromAssemblyContaining<CreateDonorValidator>();
                services.AddValidatorsFromAssemblyContaining<UpdateDonorValidator>();
                #endregion

                #region Services InKindDonationValidator
                services.AddValidatorsFromAssemblyContaining<CreateInKindDonationValidator>();
                #endregion

                #region Services InventoryValidator
                services.AddValidatorsFromAssemblyContaining<CreateInventoryValidator>();
                services.AddValidatorsFromAssemblyContaining<UpdateInventoryValidator>();
                #endregion

                #region Services ModuleByUserValidator
                services.AddValidatorsFromAssemblyContaining<CreateModuleByUserValidator>();
                #endregion

                #region Services ModuleByUserValidator
                services.AddValidatorsFromAssemblyContaining<CreateModuleByUserValidator>();
                #endregion

                #region Services MoneyDonationValidator
                services.AddValidatorsFromAssemblyContaining<CreateMoneyDonationValidator>();
                services.AddValidatorsFromAssemblyContaining<UpdateMoneyDonationValidator>();
                #endregion

                #region Services UserValidator
                services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
                services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
                services.AddValidatorsFromAssemblyContaining<LoginUserValidator>();
                #endregion

            #region Services VolunteerValidator
            services.AddValidatorsFromAssemblyContaining<CreateVolunteerValidator>();
                #endregion

            #endregion


            return services;
        }
    }
}
