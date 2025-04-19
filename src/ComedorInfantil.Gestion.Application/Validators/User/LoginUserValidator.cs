using ComedorInfantil.Gestion.Application.DTOs.User;
using ComedorInfantil.Gestion.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComedorInfantil.Gestion.Application.Validators.User
{
    public class LoginUserValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Password).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .MinimumLength(8).WithMessage("El minimo de caracteres es 8")
                .MaximumLength(255).WithMessage("El maximo de caracteres es 255");

            RuleFor(x => x.Email).NotNull().WithMessage("Este campo es requerido")
                .NotEmpty().WithMessage("Este campo es requerido")
                .EmailAddress().WithMessage("Debe ser un correo electrónico válido.")
                .MaximumLength(100).WithMessage("El maximo de caracteres es 100");
        }
    }
}
