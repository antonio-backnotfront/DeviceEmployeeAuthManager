using System.Text;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using src.DeviceEmployeeAuthManager.DAL;
using src.DeviceEmployeeAuthManager.DTO;
using src.DeviceEmployeeAuthManager.Helpers.Config;
using src.Helpers.Config;

namespace src.DeviceEmployeeAuthManager.Middlewares;

public class DeviceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DeviceMiddleware> _logger;

    public DeviceMiddleware(RequestDelegate next, ILogger<DeviceMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IDeviceValidator deviceValidator)
    {
        if ((context.Request.Method == "PUT" || context.Request.Method == "POST") &&
            context.Request.Path.StartsWithSegments("/api/devices"))
        {
            _logger.LogInformation("DeviceMiddleware started its work");
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var bodyString = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            try
            {
                var dbContext = context.RequestServices.GetRequiredService<DeviceEmployeeDbContext>(); 
                List<string> errors = await deviceValidator.ValidateAdditionalProperties(bodyString, dbContext);
                if (
                    (errors.Count > 0)
                )
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(
                        $"Additional properties contain the invalid format: {JsonSerializer.Serialize(errors)}");
                    _logger.LogInformation("DeviceMiddleware finished (DeviceProperties validated unsuccessfully)");
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 400;
                _logger.LogError("DeviceMiddleware finished catching an Exception: {0}", ex);
                await context.Response.WriteAsync("Could not validate additional properties: " + ex.Message);
                return;
            }
        }

        _logger.LogInformation("DeviceMiddleware finished successfully (DeviceProperties are validated successfully).");
        await _next(context);
    }
}