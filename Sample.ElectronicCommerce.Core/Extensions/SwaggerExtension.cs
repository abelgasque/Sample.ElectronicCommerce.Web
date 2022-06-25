using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Core.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var environmentSettingsSection = configuration.GetSection("EnvironmentSettings");
            var environmentSettings = environmentSettingsSection.Get<EnvironmentSettings>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{appSettings.Version}", new OpenApiInfo { Title = "ElectronicCommerceWS", Version = $"v{appSettings.Version}" });
                c.AddSecurityDefinition(environmentSettings.Type, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insirir um token válido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = environmentSettings.Type.ToLower(),
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = environmentSettings.Type
                                }
                            },
                            new string[]{ }
                        }
                    });
            });

            return services;
        }
    }
}
