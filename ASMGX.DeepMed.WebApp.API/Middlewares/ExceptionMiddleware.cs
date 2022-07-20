using ASMGX.DeepMed.Shared.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ASMGX.DeepMed.WebApp.API.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsJsonAsync(new Response<string>()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            Message = contextFeature.Error.Message,
                            Data = contextFeature.Error.ToString(),
                            Date = DateTime.UtcNow
                        });
                    }
                });
            });
        }
    }
}
