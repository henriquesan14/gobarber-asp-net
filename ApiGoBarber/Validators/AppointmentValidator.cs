using ApiGoBarber.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Validators
{
    public class AppointmentValidator : AbstractValidator<CreateAppointmentDTO>
    {
        public AppointmentValidator()
        {
            RuleFor(a => a.ProviderId).NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório");
            RuleFor(a => a.Date).NotNull()
                .WithMessage("O campo {PropertyName} é obrigatório");
        }
    }
}
