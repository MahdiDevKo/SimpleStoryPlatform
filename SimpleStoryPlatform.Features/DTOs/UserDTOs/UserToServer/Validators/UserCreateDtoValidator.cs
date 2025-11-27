using FluentValidation;
using FluentValidation.Internal;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators
{
    public class UserCreateDtoValidator : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidator()
        {
            Include(new UserPassValidatorRule());
            Include(new EmailValidatorRule());
            Include(new FirstNameAndLastNameValidatorRule());

            RuleFor(u => u.PasswordTwo)
                .Equal(u => u.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}
