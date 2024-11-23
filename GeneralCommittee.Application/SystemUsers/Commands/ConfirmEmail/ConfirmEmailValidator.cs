using FluentValidation;
using GeneralCommittee.Application.SystemUsers.Commands.ConfirmEmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.SystemUsers.Commands.ConfirmEmail
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailCommand>

    {
    public ConfirmEmailValidator()
    {
        RuleFor(c => c.Email)
            .EmailAddress()
            .WithMessage("Invalid email address");
        RuleFor(c => c.Email)
            .Length(3, 100);
    }
    }
}





