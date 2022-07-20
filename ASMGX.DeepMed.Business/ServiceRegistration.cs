using ASMGX.DeepMed.Business.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ASMGX.DeepMed.Business
{
    public static class ServiceRegistration
    {
        public static void AddManagers(this IServiceCollection services)
        {
            services.AddTransient<IAuthManager, AuthManager>();
        }
    }
}
