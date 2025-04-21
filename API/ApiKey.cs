namespace API
{
    public class ApiKey
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeader = "API-KEY";
        private const string ValidApiKey = "MyKeyWillBeAddHere";

        public ApiKey(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var apikey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden: Api_Key is missing");
                return;
            }
            string key = apikey.ToString();
            if (!key.Equals(ValidApiKey, StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden: invalid Api_Key, AccessDenied");
            }
            await _next(context);
        }
    }
}
