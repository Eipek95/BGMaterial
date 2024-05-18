using BGMaterial.Application.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BGMaterial.API.Filters
{
    public class ValidateFilterAttribute<T> : IAsyncActionFilter where T : class
    {
        private readonly IValidator<T> _options;

        public ValidateFilterAttribute(IValidator<T> options)
        {
            _options = options;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = (T)context.ActionArguments.Values.FirstOrDefault()!;
            var validationResult = _options.Validate(idValue);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(CustomResponseDto<T>.Fail(StatusCodes.Status400BadRequest, errors));
                return;
            }

            await next.Invoke();
        }
    }
}
