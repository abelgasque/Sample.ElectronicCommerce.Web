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
            string endPointCurrent = context.Request.Path.ToString();
            foreach (string endpoint in _appSettings.EndpointsWhiteList)
            {
                if (!endPointCurrent.Equals(endpoint))
                {
                    string authHeader = context.Request.Headers["Authorization"];
                    if (string.IsNullOrEmpty(authHeader) || (!authHeader.Contains("Bearer")))
                    {
                        throw new UnauthorizedException("Acesso não autorizado");
                    }

                }
            }
            await _next(context);
        }
    }
}