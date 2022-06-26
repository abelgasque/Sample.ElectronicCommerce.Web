using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Security.Middlewares
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EnvironmentSettings _environmenSettings;

        public BasicAuthMiddleware(
            RequestDelegate next,
            IOptions<EnvironmentSettings> environmenSettings
        )
        {
            _next = next;
            _environmenSettings = environmenSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            string endpoint = context.Request.Path.ToString();
            if ((endpoint.Contains("api/security")) || (endpoint.Contains("api/core")))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(authHeader) || (!authHeader.Contains("Basic")))
                {
                    throw new UnauthorizedException("Acesso não autorizado");
                }

                string auth = authHeader.Split(new char[] { ' ' })[1];
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                var usernameAndPassword = encoding.GetString(Convert.FromBase64String(auth));
                string username = usernameAndPassword.Split(new char[] { ':' })[0];
                string password = usernameAndPassword.Split(new char[] { ':' })[1];

                if ((!_environmenSettings.UserName.Equals(username)) || (!_environmenSettings.Password.Equals(password)))
                {
                    throw new UnauthorizedException("Acesso não autorizado");
                }
            }
            await _next(context);
        }
    }
}