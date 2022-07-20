using ASMGX.DeepMed.Shared.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ASMGX.DeepMed.WebApp.API.Middlewares
{
    public static class ValidationMiddleware
    {
        public static void AddValidationMiddleware(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var error = context.ModelState.FirstOrDefault();
                    return new JsonResult(new Response<string>(
                        data: error.Value!=null ? error.Value.Errors.First().ErrorMessage: "An error occured during the validation.",
                        message: "Validation Failed",
                        statusCode: HttpStatusCode.BadRequest));
                };
            });
        }
    }
}
