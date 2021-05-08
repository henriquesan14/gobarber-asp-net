using ApiGoBarber.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress().WithMessage("Endereço de email inválido");
            RuleFor(u => u.OldPassword)
               .MinimumLength(6).WithMessage("O campo {PropertyName} precisa ter pelo menos 6 caracteres");
            RuleFor(u => u.Password)
                .NotEmpty().When(u => u.OldPassword != null).WithMessage("O campo de senha é obrigatório quando a senha antiga for incluida")
                .MinimumLength(6).WithMessage("O campo {PropertyName} precisa ter pelo menos 6 caracteres");
        }
    }
}
