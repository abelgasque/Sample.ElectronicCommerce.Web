using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Sample.ElectronicCommerce.Security.Util
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string endPoint = httpContext.Request.Path.ToString();
            if (endPoint.Equals("/api/security/token/login"))
            {
                string authHeader = httpContext.Request.Headers["Authorization"];
                if (authHeader != null)
                {
                    string auth = authHeader.Split(new char[] { ' ' })[1];
                    Encoding encoding = Encoding.GetEncoding("UTF-8");
                    var usernameAndPassword = encoding.GetString(Convert.FromBase64String(auth));
                    string username = usernameAndPassword.Split(new char[] { ':' })[0];
                    string password = usernameAndPassword.Split(new char[] { ':' })[1];
                    if (username == "abc" && password == "123")
                    {
                        await _next(httpContext);
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 401;
                        return;
                    }
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }
            }

            await _next(httpContext);
        }
    }
}