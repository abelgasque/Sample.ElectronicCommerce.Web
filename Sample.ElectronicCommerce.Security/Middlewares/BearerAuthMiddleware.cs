using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Security.Middlewares
{
    public class BearerAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public BearerAuthMiddleware(
            RequestDelegate next,
            IOptions<AppSettings> appSettings
        )
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            string endpoint = context.Request.Path.ToString();
            if ((!endpoint.Contains("api/security")) && (!endpoint.Contains("api/core")))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(authHeader) || (!authHeader.Contains("Bearer")))
                {
                    throw new UnauthorizedException("Acesso não autorizado");
                }
            }
            await _next(context);
        }
    }
}