using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DeviceEmployeeAuthManager.Services;

namespace src.DeviceEmployeeAuthManager.Controllers;

[Authorize]
[ApiController]
[Route("/api/roles/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _service;
    private readonly ILogger<RoleController> _logger;

    public RoleController(IRoleService employeeService, ILogger<RoleController> logger)
    {
        this._logger = logger;
        this._service = employeeService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("/api/roles")]
    public async Task<IActionResult> GetAllRoles(CancellationToken ct)
    {
        try
        {
            var list = await _service.GetAllRoles(ct);
            return Ok(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }
    
}