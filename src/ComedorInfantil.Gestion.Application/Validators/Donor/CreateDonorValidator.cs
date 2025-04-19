using ComedorInfantil.Gestion.Application.DTOs.Donor;
using ComedorInfantil.Gestion.Domain.Enums;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.Donor
{
    public class CreateDonorValidator : AbstractValidator<CreateDonorDTO>
    {
        public CreateDonorValidator() 
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(50).WithMessage("El maximo de caracteres es 50");

            RuleFor(x => x.LastName).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.DonorType).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(50).WithMessage("El maximo de caracteres es 50")
                .Must(s => Enum.TryParse<TypeDonors>(s, false, out _)).WithMessage("El tipo de donante no es valido (Individual/Empresa/Institucion)");

            RuleFor(x => x.Phone).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(8).WithMessage("El maximo de caracteres es 8");

            RuleFor(x => x.Address).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");
        }
    }
}
