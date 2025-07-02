using System;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeviceEmployeeAuthManager.API.Controllers;

[Authorize]
[ApiController]
[Route("/api/roles/[controller]")]
public class RoleController : ControllerBase
{
    private readonly ILogger<RoleController> _logger;
    private readonly IRoleService _service;

    public RoleController(IRoleService employeeService, ILogger<RoleController> logger)
    {
        _logger = logger;
        _service = employeeService;
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