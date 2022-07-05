using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.DTO;
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
            string endpoint = context.Request.Path.ToString();
            string messageError = "Acesso não autorizado";
            if ((endpoint.Contains("api/security")) || (endpoint.Contains("api/core")))
            {
                string authHeader = context.Request.Headers["Authorization"];
                bool isValid = ((!string.IsNullOrEmpty(authHeader)) && (authHeader.Contains("Basic")));
                if (!isValid) throw new UnauthorizedException(messageError);
                isValid = ((_appSettings.Credentials != null) && (_appSettings.Credentials.Count > 0));
                if (!isValid) throw new UnauthorizedException(messageError);
                string auth = authHeader.Split(new char[] { ' ' })[1];
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                var usernameAndPassword = encoding.GetString(Convert.FromBase64String(auth));
                string username = usernameAndPassword.Split(new char[] { ':' })[0];
                string password = usernameAndPassword.Split(new char[] { ':' })[1];
                isValid = false;
                foreach (UserDTO credential in _appSettings.Credentials)
                {
                    isValid = credential.UserName.Equals(username) && credential.Password.Equals(password);
                    if (isValid) break;
                }
                if (!isValid) throw new UnauthorizedException(messageError);
            }
            await _next(context);
        }
    }
}