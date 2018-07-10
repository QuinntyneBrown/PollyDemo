using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace PollyDemo.Core.Extensions
{
    public static class CorsDefaults
    {
        public static readonly string Policy = "CorsPolicy";
    }

    public static class ServiceCollectionExtensions
    {        
        public static IMvcBuilder AddCustomMvc(this IServiceCollection services)
        {
            return services.AddMvc()
                .AddControllersAsServices();
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "PollyDemo",
                    Version = "v1",
                    Description = "PollyDemo REST API",
                });
                options.CustomSchemaIds(x => x.FullName);
            });

            services.ConfigureSwaggerGen();

            return services;
        }
    }
}
