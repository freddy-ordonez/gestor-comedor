using ComedorInfantil.Gestion.Application.DTOs.ModuleByUser;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.ModuleByUser
{
    public class CreateModuleByUserValidator : AbstractValidator<CreateModuleByUserDTO>
    {
        public CreateModuleByUserValidator()
        {
            RuleFor(x => x.UserId).NotNull().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser mayor a 0");

            RuleFor(x => x.ModuleId).NotNull().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser mayor a 0");
        }
    }
}
