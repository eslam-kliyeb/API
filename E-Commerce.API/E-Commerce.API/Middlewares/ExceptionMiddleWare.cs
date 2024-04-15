using E_Commerce.API.Errors;
using System.Net;
using System.Text.Json;

namespace E_Commerce.API.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionMiddleWare(RequestDelegate Next, ILogger<ExceptionMiddleWare> Logger , IHostEnvironment Environment)
        {
            _next = Next;
            _logger = Logger;
            _environment = Environment;
        }
        // invokeAsync
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            { 
                _logger.LogError(ex,ex.Message);
                // Production => Log ex in DataBase
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _environment.IsDevelopment() ?
                               new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                               new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                var Options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var jsonResponse = JsonSerializer.Serialize(response,Options);
                context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
