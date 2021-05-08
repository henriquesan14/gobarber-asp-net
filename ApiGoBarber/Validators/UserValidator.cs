using ApiGoBarber.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                    .EmailAddress().WithMessage("Endereço de email inválido");
            RuleFor(u => u.Password).NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
                .MinimumLength(6).WithMessage("O campo {PropertyName} precisa ter pelo menos 6 caracteres");
        }
    }
}
