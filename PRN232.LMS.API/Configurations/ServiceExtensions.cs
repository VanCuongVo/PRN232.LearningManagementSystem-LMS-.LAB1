using System.Text.Json.Serialization;
namespace PRN232.LMS.API.Configurations
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomJsonOptions(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    );
                });
            return services;
        }
    }
}