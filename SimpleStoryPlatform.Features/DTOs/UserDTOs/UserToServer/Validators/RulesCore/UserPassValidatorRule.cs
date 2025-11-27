using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore
{
    public class UserPassValidatorRule : AbstractValidator<IUserPassValidatorRule>
    {
        public UserPassValidatorRule()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30)
                .MinimumLength(8);

            RuleFor(u => u.Password)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(50);
        }
    }
}
