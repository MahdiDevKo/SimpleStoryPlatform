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
    public class UserSearchRequestValidator : AbstractValidator<SearchRequest<UserSearchOptionsDto>>
    {
        public UserSearchRequestValidator()
        {
            Include(new SearchRequestValidator<UserSearchOptionsDto>
                (new UserSearchOptionsDtoValidator())
                );
        }
    }
}
