namespace WebApp
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly APIClient _api;
        public AuthenticationMiddleware(RequestDelegate next, APIClient api)
        {
            _next = next;
            _api = api;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            _api.IP = context.Connection.RemoteIpAddress?.ToString();
            _api.UserAgent = context.Connection.Id.ToString();
            var token = context.Request.Cookies["token"];
            if (string.IsNullOrWhiteSpace(token) is false)
            {
                context.Request.Headers["Authorization"] = token;
            }
            // Опционально, возможна аутентификацию по дефолту
            await _next.Invoke(context);
        }
    }
}