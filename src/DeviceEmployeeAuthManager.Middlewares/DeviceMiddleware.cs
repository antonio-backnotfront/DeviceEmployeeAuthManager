namespace src.DeviceEmployeeAuthManager.Middlewares;

public class DeviceMiddleware
{
    private readonly RequestDelegate _next;

    public DeviceMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        if ((context.Request.Method == "PUT" || context.Request.Method == "POST") && context.Request.Path.StartsWithSegments("/api/devices/"))
        {
            string body = new StreamReader(context.Request.Body).ReadToEnd();
            Console.WriteLine(body);
            Console.WriteLine("context.Request.QueryString " + context.Request.Path);
        }
        await _next(context);
    }
}