using ComedorInfantil.Gestion.Application.DTOs.MoneyDonation;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.MoneyDonation
{
    public class UpdateMoneyDonationValidator : AbstractValidator<UpdateMoneyDonationDTO>
    {
        public UpdateMoneyDonationValidator()
        {
            RuleFor(x => x.Porpuse).NotNull().WithMessage("Este campo es requerido")
              .NotEmpty().WithMessage("Este campo es requerido")
              .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.DonationDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");

            RuleFor(x => x.DonationDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");
        }
    }
}
