public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log request details
        var method = context.Request.Method;
        var path = context.Request.Path;

        Console.WriteLine($"[Request] Method: {method}, Path: {path}");

        // Call the next middleware in the pipeline
        await _next(context);

        // Log response details
        var statusCode = context.Response.StatusCode;
        Console.WriteLine($"[Response] Status Code: {statusCode}");
    }
}
