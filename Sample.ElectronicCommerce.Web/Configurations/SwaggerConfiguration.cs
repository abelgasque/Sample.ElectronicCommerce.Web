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

            var securitySettingsSection = configuration.GetSection("SecuritySettings");
            var securitySettings = securitySettingsSection.Get<SecuritySettings>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{appSettings.Version}", new OpenApiInfo { Title = "ElectronicCommerceWS", Version = $"v{appSettings.Version}" });
                c.AddSecurityDefinition(securitySettings.Type, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Insirir um token válido",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = securitySettings.Type.ToLower(),
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = securitySettings.Type
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
