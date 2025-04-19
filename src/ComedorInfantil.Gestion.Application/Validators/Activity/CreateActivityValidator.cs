using ComedorInfantil.Gestion.Application.DTOs.Activity;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.Activity
{
    public class CreateActivityValidator : AbstractValidator<CreateActivityDTO>
    {
        public CreateActivityValidator() 
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(100).WithMessage("El maximo de caracteres es 100");

            RuleFor(x => x.Description).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.StartDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");

            RuleFor(x => x.EndDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");
        }
    }
}
