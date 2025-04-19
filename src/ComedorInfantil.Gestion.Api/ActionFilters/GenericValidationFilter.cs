using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ComedorInfantil.Gestion.Api.ActionFilters
{
    public class GenericValidationFilter : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public GenericValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType);

                if (validator != null)
                {
                    var validateMethod = validatorType.GetMethod("Validate", new[] { argument.GetType() });
                    var result = (ValidationResult)validateMethod.Invoke(validator, new[] { argument });

                    if (!result.IsValid)
                    {
                        var errores = result.Errors.Select(e => new
                        {
                            Campo = e.PropertyName,
                            Mensaje = e.ErrorMessage
                        });

                        context.Result = new BadRequestObjectResult(errores);
                        return;
                    }
                }
            }
        }
    }
}
