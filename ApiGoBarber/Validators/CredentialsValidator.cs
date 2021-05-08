using ApiGoBarber.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Validators
{
    public class CredentialsValidator : AbstractValidator<UserCredentialsDTO>
    {
        public CredentialsValidator()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                    .EmailAddress().WithMessage("Endereço de email inválido");
            RuleFor(c => c.Password).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
                
        }
    }
}
