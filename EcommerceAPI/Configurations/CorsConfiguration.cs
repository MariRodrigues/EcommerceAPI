using Microsoft.Extensions.DependencyInjection;

namespace EcommerceAPI.Configurations
{
    public static class CorsConfiguration
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(setup =>
            {
                setup.AddPolicy("DefaultCors", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            return services;
        }
    }
}
