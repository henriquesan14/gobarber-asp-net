using Microsoft.Extensions.DependencyInjection;

namespace ApiGoBarber.Extensions
{
    public static class SerializationExtensions
    {
        public static IServiceCollection JsonSerializationConfig(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            return services;
        }
    }
}
