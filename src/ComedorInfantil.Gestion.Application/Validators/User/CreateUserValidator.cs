using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Domain.Enums;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Este campo es requerido")
               .NotEmpty().WithMessage("Este campo es requerido")
               .MaximumLength(50).WithMessage("El maximo de caracteres es 50");

            RuleFor(x => x.LastName).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(50).WithMessage("El maximo de caracteres es 50");

            RuleFor(x => x.Password).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MinimumLength(8).WithMessage("El minimo de caracteres es 8")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.Email).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .EmailAddress().WithMessage("Debe ser un correo electrónico válido.")
                .MaximumLength(100).WithMessage("El maximo de caracteres es 100");

            RuleFor(x => x.Status).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(20).WithMessage("El maximo de caracteres es 20")
                .Must(s => Enum.TryParse<TypeStatus>(s, false, out _)).WithMessage("El tipo de Estatus no es valido (Activo/Inactivo)");
        }
    }
}
