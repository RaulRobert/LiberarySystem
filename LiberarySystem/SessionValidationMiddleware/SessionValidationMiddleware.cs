using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace library_system.Middleware
{
    public class SessionValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            // Allow access to SignIn, SignUp, or static files
            if (path != null &&
                (path.Contains("/authentications/signin") ||
                 path.Contains("/authentications/signup") ||
                 path.Contains("/css") ||
                 path.Contains("/js") ||
                 path.Contains("/images")))
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


            if (path == "/" && (referer) != "/")
            {
                path = referer.ToLower();
            }

            if (path.StartsWith("/authentications"))
            {
                await _next(context);
                return;
            }
            // Check session for logged-in user
            var username = context.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                // Not logged in → redirect to SignIn
                context.Response.Redirect("/Authentications/SignIn");
                return;
            }

            // User is logged in → continue request
            await _next(context);
        }
    }
}
