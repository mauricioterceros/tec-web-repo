using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Services.Exceptions;

namespace PresentationLayer.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DemoPracticeExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public DemoPracticeExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                Console.WriteLine("ANTES DEL NEXT");
                await _next(httpContext);
                Console.WriteLine("DESPUES DEL NEXT");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            ExceptionResponseWrapper exceptionWrapper = new ExceptionResponseWrapper();
            string errorMessage = "";
            int statusCode = (int)HttpStatusCode.OK;

            // HTTP-200 (OK) => para toda respuesta positiva (por defecto), o para todo error controlado
            // HTTP-401 (Unauthorized) => no tiene acceso a nuestra app
            // HTTP-500 (InternalServerError) => para todo error no controlado
            // HTTP STATUS CODES
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Status

            if (ex is UnauthorizedAccessException)
            {
                exceptionWrapper.Code = (int)HttpStatusCode.Unauthorized;
                exceptionWrapper.Message = "NO TIENES ACCESO";
            }
            else if (ex.InnerException is NumberServiceException)
            {
                exceptionWrapper.Code = (int)HttpStatusCode.OK;
                exceptionWrapper.Message = "Hay errores de coneccion con servicios externos. MORE DETAIL: " + ex.Message;
            }
            else 
            {
                exceptionWrapper.Code = (int)HttpStatusCode.InternalServerError;
                exceptionWrapper.Message = "un error inesperado ha sucedido: " + ex.Message;
            }

            // httpContext.Response.ContentType = "text/plain";
            httpContext.Response.ContentType = "application/json"; // => all success responses from endpoints (by default)
            httpContext.Response.Headers["Accept"] = "application/json";
            httpContext.Response.StatusCode = statusCode;
            // CONTENT TYPES
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Type
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Accept
            // https://www.ibm.com/docs/en/order-management?topic=services-specifying-http-headers

            Log.Error("Suecido un error en la aplicacion" + ex.Message);

            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(exceptionWrapper));
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class DemoPracticeExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseDemoPracticeExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DemoPracticeExceptionMiddleware>();
        }
    }
}
