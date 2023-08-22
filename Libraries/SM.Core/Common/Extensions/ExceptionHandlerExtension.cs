using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace SM.Core.Common.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ApiResponse<object>
                        {
                            Success = false,
                            Message = contextFeature.Error.Message,
                            Data = null,
                            Errors = null
                        }));
                    }
                });
            });
        }
    }
}
