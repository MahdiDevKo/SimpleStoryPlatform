using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators
{
    public class StoryReviewCreateDtoValidator : AbstractValidator<StoryReviewCreateDto>
    {
        public StoryReviewCreateDtoValidator()
        {
            RuleFor(u => u.Score)
                .GreaterThanOrEqualTo(0f)
                .WithMessage("score cant be less than 0")
                .LessThanOrEqualTo(5f)
                .WithMessage("score cant be greater than 5");

            RuleFor(u => u.Data)
                .MaximumLength(150)
                .WithMessage("maximum length: 150");
        }
    }
}
