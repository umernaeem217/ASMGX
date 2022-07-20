using ASMGX.DeepMed.Application.Shared.Domain.Concrete;
using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ASMGX.DeepMed.Application
{
    public static class ServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
