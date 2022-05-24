using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace PresentationLayer.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private ISessionManager _authorizationService;

        public AuthenticationMiddleware(RequestDelegate next, ISessionManager sessionManager)
        {
            _next = next;
            _authorizationService = sessionManager;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (
               httpContext.Request.Path.Value.ToLower() == "/swagger" ||
               httpContext.Request.Path.Value.ToLower() == "/swagger/index.html" ||
               httpContext.Request.Path.Value.ToLower() == "/swagger/v1/swagger.json" ||
               httpContext.Request.Path.Value.ToLower() == "/swagger/swagger-ui.css" ||
               httpContext.Request.Path.Value.ToLower() == "/swagger/swagger-ui-bundle.js" ||
               httpContext.Request.Path.Value.ToLower() == "/swagger/swagger-ui-standalone-preset.js")
            {
                await _next(httpContext);
            }
            else
            {
                // [FromHeader] string userName, [FromHeader] string password
                string userName = httpContext.Request.Headers["userName"];
                string password = httpContext.Request.Headers["password"];
                if (_authorizationService.ValidateCredentials(userName, password) != null)
                {
                    await _next(httpContext);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DemoCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
