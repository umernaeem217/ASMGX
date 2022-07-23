using ASMGX.DeepMed.Application.Authentication;
using ASMGX.DeepMed.Application.General;
using ASMGX.DeepMed.Application.Shared.Domain.Concrete;
using ASMGX.DeepMed.Application.Shared.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ASMGX.DeepMed.Application
{
    public static class ServiceRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ILookupRepository, LookupRepository>();
        }
    }
}
