namespace WebApp
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
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