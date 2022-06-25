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
        private readonly AppSettings _appSettings;

        public BasicAuthMiddleware(
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
                if (endPointCurrent.Equals(endpoint))
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
                    bool hasAppAuth = (_appSettings.Name.Equals(username) && _appSettings.Code.Equals(password));

                    if (!hasAppAuth)
                    {
                        throw new UnauthorizedException("Acesso não autorizado");
                    }
                }
            }
            await _next(context);
        }
    }
}