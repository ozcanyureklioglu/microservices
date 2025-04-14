using Microsoft.Extensions.Options;

namespace MS.Catalog.Api.Options
{
    public static class OptionExtention
    {
        public static IServiceCollection AddDbOptions(this IServiceCollection services)
        {
            services.AddOptions<MongoOptions>().BindConfiguration(nameof(MongoOptions)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOptions>>().Value);
            return services;
        }
    }
}
