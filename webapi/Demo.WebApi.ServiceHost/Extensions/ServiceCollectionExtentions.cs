using Microsoft.OpenApi.Models;

namespace Demo.WebApi.ServiceHost.Extensions
{
    public static class ServiceCollectionExtentions
    {
        private const string SwagerTitle = "Demo API";
        private const string AppVersion = "v1";

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(
                    AppVersion,
                    new OpenApiInfo
                    {
                        Title = SwagerTitle,
                        Version = AppVersion
                    });
            });
        }
    }
}
