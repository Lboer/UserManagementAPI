public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;

    public TokenValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check for Authorization header
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("{\"error\": \"Authorization header missing.\"}");
            return;
        }

        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        // Validate token (replace this with real validation logic)
        if (string.IsNullOrEmpty(token) || !IsValidToken(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("{\"error\": \"Invalid or missing token.\"}");
            return;
        }

        // If valid, continue to next middleware
        await _next(context);
    }

    private bool IsValidToken(string token)
    {
        // Simple placeholder logic: In real-world, validate JWT or custom token
        return token == "my-secret-token"; // Replace with actual validation
    }
}
