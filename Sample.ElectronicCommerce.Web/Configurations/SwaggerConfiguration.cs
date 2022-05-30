using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sample.ElectronicCommerce.Shared.Entities.Settings;

namespace Sample.ElectronicCommerce.Web.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection SetSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();

            var tokenSettingsSection = configuration.GetSection("TokenSettings");
            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{appSettings.Version}", new OpenApiInfo { Title = "ElectronicCommerceWS", Version = $"v{appSettings.Version}" });
                c.AddSecurityDefinition(tokenSettings.Type, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insirir um token válido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = tokenSettings.Type.ToLower(),
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = tokenSettings.Type
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
