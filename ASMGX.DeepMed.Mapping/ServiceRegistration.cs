using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ASMGX.DeepMed.Mapping
{
    public static class ServiceRegistration
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
