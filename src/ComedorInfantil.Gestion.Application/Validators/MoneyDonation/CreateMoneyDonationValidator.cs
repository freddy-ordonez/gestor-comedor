using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.MoneyDonation
{
    public class CreateMoneyDonationValidator : AbstractValidator<CreateMoneyDonationDTO>
    {
        public CreateMoneyDonationValidator()
        {
            RuleFor(x => x.Porpuse).NotNull().WithMessage("Este campo es requerido")
              .NotEmpty().WithMessage("Este campo es requerido")
              .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.Amount).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1000).WithMessage("La donacion tiene que ser mayor a 1000");


            RuleFor(x => x.DonorId).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser mayor a 0");

            RuleFor(x => x.DonationDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");
        }
    }
}
