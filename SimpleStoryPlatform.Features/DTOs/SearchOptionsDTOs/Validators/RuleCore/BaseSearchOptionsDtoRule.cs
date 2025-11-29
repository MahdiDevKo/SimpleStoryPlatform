using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs.Validators.RuleCore
{
    public class BaseSearchOptionsDtoRule : AbstractValidator<BaseSearchOptionsDto>
    {
        public BaseSearchOptionsDtoRule()
        {
            RuleFor(x => x.CreatedDateFrom)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Start date cannot be in the future.");

            RuleFor(x => x.CreatedDateTo)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("End date cannot be in the future.")
                .GreaterThanOrEqualTo(x => x.CreatedDateFrom)
                .When(x => x.CreatedDateFrom.HasValue)
                .WithMessage("End date cannot be earlier than start date.");
        }
    }
}
