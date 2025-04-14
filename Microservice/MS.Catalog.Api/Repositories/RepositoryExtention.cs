using MongoDB.Driver;
using MS.Catalog.Api.Options;
using System.Runtime.CompilerServices;

namespace MS.Catalog.Api.Repositories
{
    public static class RepositoryExtention
    {
        public static IServiceCollection AddDbExt(this IServiceCollection service)
        {
            service.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                var option = sp.GetRequiredService<MongoOptions>();
                return new MongoClient(option.ConnectionString);
            });
            service.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<MongoClient>();
                var options = sp.GetRequiredService<MongoOptions>();
                return AppDbContext.Create(mongoClient.GetDatabase(options.ConnectionString));
            });

            return service;
        }
    }
}
