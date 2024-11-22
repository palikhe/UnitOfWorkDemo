using Microsoft.Extensions.DependencyInjection;
using Database;

namespace Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddDatabaseServices();
            return services;
        }
    }
}
