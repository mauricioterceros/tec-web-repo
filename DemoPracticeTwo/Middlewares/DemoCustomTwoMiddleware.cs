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
    public class DemoCustomTwoMiddleware
    {
        private readonly RequestDelegate _next;
        private ISessionManager _authorizationService;

        public DemoCustomTwoMiddleware(RequestDelegate next, ISessionManager sessionManager)
        {
            _next = next;
            _authorizationService = sessionManager;
        }

        public async Task Invoke(HttpContext httpContext)
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

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DemoCustomTwoMiddlewareExtensions
    {
        public static IApplicationBuilder UseDemoCustomTwoMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DemoCustomTwoMiddleware>();
        }
    }
}
