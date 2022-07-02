using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using System.Text;

namespace Sample.ElectronicCommerce.Security.Extensions
{
    public static class JsonWebTokenExtension
    {
        public static IServiceCollection AddJsonWebToken(this IServiceCollection services, IConfiguration configuration)
        {
            var tokenSettingsSection = configuration.GetSection("TokenSettings");
            var tokenSettings = tokenSettingsSection.Get<TokenSettings>();

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
