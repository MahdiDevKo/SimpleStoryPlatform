using FluentValidation;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer;
using SimpleStoryPlatform.Application.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs.Validators.SearchRequestValidators
{
    public class SearchRequestValidator<TSearchOptions> : AbstractValidator<SearchRequest<TSearchOptions>>
        where TSearchOptions : class
    {
        public SearchRequestValidator(IValidator<TSearchOptions> optionsValidator)
        {
            RuleFor(x => x.Options).NotNull().WithMessage("Search options cannot be null.");

            RuleFor(x => x.Options).SetValidator(optionsValidator);

            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
