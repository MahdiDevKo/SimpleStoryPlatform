using FluentValidation;
using SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs.Validators.RuleCore;
using SimpleStoryPlatform.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs.Validators.SearchRequestValidators
{
    public class StorySearchRequestValidator : AbstractValidator<SearchRequest<StorySearchOptionsDto>>
    {
        public StorySearchRequestValidator()
        {
            Include(new SearchRequestValidator<StorySearchOptionsDto>
                (new StorySearchOptionsDtoValidator())
                );
        }
    }
}
