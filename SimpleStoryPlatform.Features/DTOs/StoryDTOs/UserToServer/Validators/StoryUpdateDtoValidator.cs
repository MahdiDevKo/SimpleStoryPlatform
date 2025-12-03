using FluentValidation;
using FluentValidation.Internal;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.ServerToUser;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators
{
    public class StoryUpdateDtoValidator : AbstractValidator<StoryUpdateDto>
    {
        public StoryUpdateDtoValidator()
        {
            Include(new StoryNameAndPreivewRule());

            RuleForEach(x => x.Data)
                .SetValidator(new StorySectionRule());
        }
    }
}
