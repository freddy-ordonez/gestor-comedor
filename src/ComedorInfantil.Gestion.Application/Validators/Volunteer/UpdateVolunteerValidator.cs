using ComedorInfantil.Gestion.Application.DTOs.Volunteer;
using ComedorInfantil.Gestion.Domain.Enums;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.Volunteer
{
    public class UpdateVolunteerValidator : AbstractValidator<UpdateVolunteerDTO>
    {
        public UpdateVolunteerValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("Este campo es requerido")
               .NotEmpty().WithMessage("Este campo es requerido")
               .MaximumLength(50).WithMessage("El maximo de caracteres es 50");

            RuleFor(x => x.LastName).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.TypeIdentification).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser igual o mayor a 1");

            RuleFor(x => x.Identification).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(20).WithMessage("El maximo de caracteres es 20");

            RuleFor(x => x.Phone).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(8).WithMessage("El maximo de caracteres es 8");

            RuleFor(x => x.Availability).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(150).WithMessage("El maximo de caracteres es 150");

            RuleFor(x => x.Status).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(20).WithMessage("El maximo de caracteres es 20")
                .Must(s => Enum.TryParse<TypeStatus>(s, false, out _)).WithMessage("El tipo de Estatus no es valido (Activo/Inactivo)");
        }
    }
}
