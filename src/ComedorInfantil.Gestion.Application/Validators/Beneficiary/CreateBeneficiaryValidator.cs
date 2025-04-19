using ComedorInfantil.Gestion.Application.DTOs.Beneficiary;
using ComedorInfantil.Gestion.Domain.Enums;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.Beneficiary
{
    public class CreateBeneficiaryValidator : AbstractValidator<CreateBeneficiaryDTO>
    {
        public CreateBeneficiaryValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(50).WithMessage("El maximo de caracteres es 50");

            RuleFor(x => x.LastName).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.Status).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(20).WithMessage("El maximo de caracteres es 20")
                .Must(s => Enum.TryParse<TypeStatus>(s, false, out _)).WithMessage("El tipo de Estatus no es valido (Activo/Inactivo)");

            RuleFor(x => x.BirthDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");
        }
    }
}
