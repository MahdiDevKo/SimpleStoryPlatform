using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore
{
    public class FirstNameAndLastNameValidatorRule : AbstractValidator<IFirstNameAndLastNameValidatorRule>
    {
        public FirstNameAndLastNameValidatorRule()
        {
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .MinimumLength(8);

            RuleFor(u => u.LastName)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(50);
        }
    }
}
