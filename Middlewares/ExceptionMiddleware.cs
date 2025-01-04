using DR_Tic_Tac_Toe.Common.Errors;
using System.Net;

namespace DR_Tic_Tac_Toe.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(new HttpError()
            {
                StatusCode = context.Response.StatusCode,
                Message = "We apologize but we experienced some server errors. Dedicated team is notified. Please try again."
            }.ToString());
        }
    }
}
