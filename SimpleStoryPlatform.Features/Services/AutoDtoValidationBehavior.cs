using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SimpleStoryPlatform.Application.Responses;

namespace SimpleStoryPlatform.Application.Services
{
    public class AutoDtoValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResponse, new()
    {
        private readonly IServiceProvider _provider;

        public AutoDtoValidationBehavior(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var errors = new List<string>();

            //check all of the properties of request (for getting a dto that has a validator)
            var properties = typeof(TRequest).GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(request);
                if (value == null)
                    continue;

                var valueType = value.GetType();

                // is there any validator for this prop?
                var validatorType = typeof(IValidator<>).MakeGenericType(valueType);
                var validator = _provider.GetService(validatorType) as IValidator;

                if (validator == null)
                    continue; //there is no validator for this prop

                //validate the prop 
                var context = new ValidationContext<object>(value);
                var result = validator.Validate(context);

                if (!result.IsValid)
                {
                    errors.AddRange(result.Errors.Select(e => e.ErrorMessage));
                }
            }

            if (errors.Any())
            {
                return new TResponse
                {
                    Success = false,
                    Errors = errors,
                    Message = "Validation failed"
                };
            }

            return await next();
        }
    }


}
