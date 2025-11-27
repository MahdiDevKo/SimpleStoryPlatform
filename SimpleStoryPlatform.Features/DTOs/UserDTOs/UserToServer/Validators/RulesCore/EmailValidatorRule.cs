using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore
{
    public class EmailValidatorRule : AbstractValidator<IEmailValidatorRule>
    {
        public EmailValidatorRule()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();
        }
    }
}
