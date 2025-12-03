using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleStoryPlatform.Application.DTOs.SearchOptionsDTOs.Validators.SearchRequestValidators;
using SimpleStoryPlatform.Application.DTOs.StoryDTOs.UserToServer.Validators;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators;
using SimpleStoryPlatform.Application.DTOs.UserDTOs.UserToServer.Validators.RulesCore;
using SimpleStoryPlatform.Application.Services;
using System.Reflection;
namespace SimpleStoryPlatform.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(cfg =>
            {
                cfg.AddMaps(typeof(ApplicationServicesRegistration).Assembly);
            });

            // MediatR
            services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //validators registration
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AutoDtoValidationBehavior<,>));
            services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<UserPassValidatorRule>();
            services.AddValidatorsFromAssemblyContaining<StoryCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<StoryReviewCreateDtoValidator>();
            services.AddValidatorsFromAssemblyContaining<StoryUpdateDtoValidator>();

            services.AddValidatorsFromAssemblyContaining<StorySearchRequestValidator>();
            services.AddValidatorsFromAssemblyContaining<UserSearchRequestValidator>();

            return services;
        }
    }
}
