using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using MongoDB.Driver.Linq;
using TransactionHandlerBasic;

namespace Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var mongoCS = "Use the connection string you own";
                var mongoClientSettings = MongoClientSettings.FromConnectionString(mongoCS);
                mongoClientSettings.LinqProvider = LinqProvider.V3;
                mongoClientSettings.ClusterConfigurator = cb =>
                {
                    cb.Subscribe<CommandStartedEvent>(e => Console.WriteLine($"{e.CommandName} - {e.Command.ToJson()}"));
                };
                return new MongoClient(mongoClientSettings);
            });

            // Add Transaction as a scoped service, accessible via any of its implemented interfaces
            services.AddScoped<ITransaction, MongoDbTransaction>();
            services.AddScoped(sp => (ITransactionIdProvider<IClientSessionHandle?>)sp.GetRequiredService<ITransaction>());

            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}
