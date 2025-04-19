using ComedorInfantil.Gestion.Application.DTOs.InKindDonation;
using FluentValidation;

namespace ComedorInfantil.Gestion.Application.Validators.InKindDonation
{
    public class CreateInKindDonationValidator : AbstractValidator<CreateInKindDonationDTO>
    {
        public CreateInKindDonationValidator()
        {
            RuleFor(x => x.ProductId).NotNull().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser mayor a 0");

            RuleFor(x => x.DonorId).NotNull().WithMessage("Este campo es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El campo tiene que ser mayor a 0");

            RuleFor(x => x.DonationDate).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido");
        }
    }
}
