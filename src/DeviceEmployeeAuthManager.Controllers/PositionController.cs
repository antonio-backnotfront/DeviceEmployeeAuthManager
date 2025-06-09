using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DeviceEmployeeAuthManager.Services;

namespace src.DeviceEmployeeAuthManager.Controllers;

[Authorize]
[ApiController]
[Route("/api/positions/[controller]")]
public class PositionController : ControllerBase
{
    private readonly IPositionService _service;
    private readonly ILogger<PositionController> _logger;

    public PositionController(IPositionService positionService, ILogger<PositionController> logger)
    {
        this._logger = logger;
        this._service = positionService;
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
            return Problem(detail: ex.Message);
        }
    }
    
}