using FluentValidation;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore;
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
            Include(new StoryNameAndPreivewRule());
        }
    }
}
