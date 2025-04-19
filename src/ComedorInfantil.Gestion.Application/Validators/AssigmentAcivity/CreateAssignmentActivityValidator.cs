using ComedorInfantil.Gestion.Application.DTOs.AssigmentAcivity;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.AssigmentAcivity
{
    public class CreateAssignmentActivityValidator : AbstractValidator<CreateAssigmentActivityDTO>
    {
        public CreateAssignmentActivityValidator()
        {
            RuleFor(x => x.ActivityId).NotNull().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser mayor a 0");

            RuleFor(x => x.VolunteerId).NotNull().WithMessage("La Descripcion es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser mayor a 0");

            RuleFor(x => x.AssignmentDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");
        }
    }
}
