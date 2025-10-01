namespace library_system.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? string.Empty;

            // Allow static files
            if (path.EndsWith(".css") || path.EndsWith(".js") || true)
            {
                await _next(context);
                return;
            }

            string referer = "?";
            if (context.Request.Method.ToLower() != "get")
            {
                Uri.TryCreate(context.Request.Headers.Referer.ToString(), new UriCreationOptions(), out var res);
                referer = res?.AbsolutePath ?? "/";
            }


            if(path == "/" && (referer)!= "/")
            {
                path = referer.ToLower();
            }


            // Public pages (global, outside areas)
            if (path.StartsWith("/authentications"))
            {
                await _next(context);
                return;
            }

            // Check if session token exists
            var token = context.Session.GetString("UserToken");
            if (string.IsNullOrEmpty(token))
            {
                // Always redirect to global Authentications/SignIn
                context.Response.Redirect("/Authentications/SignIn");
                return;
            }

            await _next(context);
        }
    }
}
