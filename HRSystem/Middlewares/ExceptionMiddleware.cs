using HRSystem.Error_Handling;
using System.Net;
using System.Text.Json;

namespace HRSystem.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            this.next = next;
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
               await next.Invoke(context); //go to next middelware
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
              var response=   environment.IsDevelopment() ? new ApiExseptionResponse(500,ex.Message,ex.InnerException.ToString()) : new ApiExseptionResponse(500);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

              var jsonResponse = JsonSerializer.Serialize(response, options);
               await context.Response.WriteAsync(jsonResponse);
            }
        
        
        
        }
    }
}
