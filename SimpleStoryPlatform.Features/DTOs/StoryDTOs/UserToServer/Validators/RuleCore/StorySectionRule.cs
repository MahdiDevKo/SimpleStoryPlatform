using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators.RuleCore
{
    public class StorySectionRule : AbstractValidator<IStorySectionRule>
    {
        public StorySectionRule()
        {
            RuleFor(ss => ss.Narration)
                .MaximumLength(1000)
                .WithMessage("character limit reached... (Max: 1000)")
                .NotNull()
                .WithMessage("write somthing... (its null)");

            RuleFor(u => u.BGImageUrl)
                .Must(url =>
                url == null ||
                (Uri.IsWellFormedUriString(url, UriKind.Absolute) &&
                url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                )
                .WithMessage("ImageUrl must be a valid absolute URL ending with.jpg");

            RuleFor(u => u.BGMusicUrl)
                .Must(url =>
                url == null ||
                (Uri.IsWellFormedUriString(url, UriKind.Absolute) &&
                url.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
                )
                .WithMessage("ImageUrl must be a valid absolute URL ending with .mp3");
        }
    }
}
