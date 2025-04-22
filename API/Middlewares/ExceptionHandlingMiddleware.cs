using Newtonsoft.Json;

namespace API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // 记录异常信息（可以使用日志框架记录异常）

            // 根据异常类型返回适当的状态码和错误消息
            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case KeyNotFoundException _:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case ArgumentException _:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case UnauthorizedAccessException _:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case InvalidOperationException _:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                // Add more specific exceptions as needed
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = ex.Message }));
        }
    }
}