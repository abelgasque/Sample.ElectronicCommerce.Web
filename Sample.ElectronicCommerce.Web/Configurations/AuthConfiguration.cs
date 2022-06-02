using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using System.Text;

namespace Sample.ElectronicCommerce.Web.Configurations
{
    public static class AuthConfiguration
    {
        public static IServiceCollection SetAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var securitySettingsSection = configuration.GetSection("SecuritySettings");
            var securitySettings = securitySettingsSection.Get<SecuritySettings>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securitySettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
