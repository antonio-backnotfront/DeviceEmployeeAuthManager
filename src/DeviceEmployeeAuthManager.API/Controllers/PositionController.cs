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
[Route("/api/positions/[controller]")]
public class PositionController : ControllerBase
{
    private readonly ILogger<PositionController> _logger;
    private readonly IPositionService _service;

    public PositionController(IPositionService positionService, ILogger<PositionController> logger)
    {
        _logger = logger;
        _service = positionService;
    }

    [HttpGet("/api/positions")]
    public async Task<IActionResult> GetAllRoles(CancellationToken ct)
    {
        try
        {
            var list = await _service.GetAllPositions(ct);
            return Ok(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }
}