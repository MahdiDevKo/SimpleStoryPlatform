using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs.Validators.RuleCore
{
    public class StorySearchOptionsDtoValidator : AbstractValidator<StorySearchOptionsDto>
    {
        public StorySearchOptionsDtoValidator()
        {
            Include(new BaseSearchOptionsDtoRule());

            RuleFor(x => x.StoryName)
                .MaximumLength(150)
                .WithMessage("story name cant be greater than 150 characters");
        
            RuleFor(x => x.WriterUsername)
                .MaximumLength(30)
                .WithMessage("writer name cant be greater than 30 characters");
        }
    }
}
