using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators
{
    public class StoryCreateDtoValidator : AbstractValidator<StoryCreateDto>
    {
        public StoryCreateDtoValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(1)
                .WithMessage("minimum length: 1")
                .MaximumLength(50)
                .WithMessage("maximum length: 50");

            RuleFor(u => u.Preview)
                .NotEmpty()
                .NotNull()
                .MinimumLength(20)
                .WithMessage("write at least 20 character about your story")
                .MaximumLength(300)
                .WithMessage("maximum length: 300");
        }
    }
}
