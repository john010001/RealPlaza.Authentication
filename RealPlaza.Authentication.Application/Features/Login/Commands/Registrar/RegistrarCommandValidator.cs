using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Features.Login.Commands.Registrar
{
    public class RegistrarCommandValidator : AbstractValidator<RegistrarCommand>
    {
        public RegistrarCommandValidator() 
        {
          

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Ingrese una contraseña.")
                .NotNull()
                .MaximumLength(8).WithMessage("{Password} No puedes exceder de los 8 caracteres.")
                .MinimumLength(8).WithMessage("{Password} La contraseña debe tener al menos 8 caracteres.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                    .WithMessage("{Password} La contraseña debe contener al menos una letra mayúscula, una letra minúscula, un número y un carácter especial.");
        }
    }
}
