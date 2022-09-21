using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace Hotel.Management.WebApi.Middleware
{
    public static class UnhandledExceptionMiddleware
    {
        public static IApplicationBuilder UseUnhandledExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;
                    context.Response.StatusCode = exception is UnauthorizedAccessException ? 401 : 400;
                    return Task.CompletedTask;
                });
            });
        }
    }
}