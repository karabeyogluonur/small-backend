using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SM.Core.Common.Behaviors
{
    public class ValidationBehavior : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
                await next();

            var errors = context.ModelState.Where(modelState => modelState.Value.Errors.Any())
                .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
                .ToArray();
            
            context.Result = new BadRequestObjectResult(ApiResponse<object>.Error(errors, "The sent object is incorrect."));

            return;
        }

    }
}
