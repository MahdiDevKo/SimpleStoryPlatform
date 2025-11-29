using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs.Validators.RuleCore
{
    public class UserSearchOptionsDtoValidator : AbstractValidator<UserSearchOptionsDto>
    {
        public UserSearchOptionsDtoValidator()
        {
            Include(new BaseSearchOptionsDtoRule());

            RuleFor(x => x.FirstName)
                .MaximumLength(30)
                .WithMessage("firstname name cant be greater than 150 characters");

            RuleFor(x => x.LastName)
                .MaximumLength(30)
                .WithMessage("lastname name cant be greater than 30 characters");
        
            RuleFor(x => x.Username)
                .MaximumLength(30)
                .WithMessage("username name cant be greater than 30 characters");
        }
    }
}
