using ComedorInfantil.Gestion.Application.DTOs.Inventory;
using ComedorInfantil.Gestion.Domain.Enums;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.Inventory
{
    public class CreateInventoryValidator : AbstractValidator<CreateInventoryDTO>
    {
        public CreateInventoryValidator()
        {
            RuleFor(x => x.ProductName).NotNull().WithMessage("Este campo es requerido")
               .NotEmpty().WithMessage("Este campo es requerido")
               .MaximumLength(100).WithMessage("El maximo de caracteres es 100");

            RuleFor(x => x.Description).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.Quantity).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser igual o mayor a 1");

            RuleFor(x => x.EntryDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");

            RuleFor(x => x.ExpiryDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");
        }
    }
}
