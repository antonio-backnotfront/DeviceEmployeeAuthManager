using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DeviceEmployeeAuthManager.DAL;
using src.DeviceEmployeeAuthManager.DTO;
using src.DeviceEmployeeAuthManager.Exceptions;
using src.DeviceEmployeeAuthManager.Services;

namespace src.DeviceEmployeeAuthManager.Controllers;


[ApiController]
[Route("/api/devices/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    private readonly IAccountService _accountService;

    public DeviceController(IDeviceService deviceService, IAccountService accountService)
    {
        this._deviceService = deviceService;
        this._accountService = accountService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("/api/devices/")]
    public async Task<IActionResult> GetDevices(CancellationToken ct)
    {
        try
        {
            var results = await _deviceService.GetAllDevices(ct);
            return results.Count > 0 ? Ok(results) : NotFound("No devices found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    [Authorize(Roles = "Admin, User")]
    [HttpGet("/api/devices/{id}")]
    public async Task<IActionResult> GetDevice(int id, CancellationToken ct)
    {
        try
        {
            var currentUserName = User.Identity?.Name;
            if (currentUserName == null)
                return Forbid();
            var currentUser = await _accountService.GetAccountByUsername(currentUserName, ct);
            if (currentUser.Role.Name != "Admin")
            {
                var deviceIdsByEmployee = await _deviceService.GetDeviceIdsByEmployeeId(currentUser.EmployeeId, ct);
                if (!deviceIdsByEmployee.Contains(id))
                    return Forbid();
            }
            
            var device = await _deviceService.GetDeviceById(id,ct);
            return device is not null ? Ok(device) : NotFound("Device not found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("/api/devices/")]
    public async Task<IActionResult> AddDevice(CreateDeviceDto dto, CancellationToken ct)
    {
        try
        {
            await _deviceService.CreateDevice(dto, ct);
            return Created("/api/devices", dto);
        }
        catch (InvalidDeviceTypeException ex)
        {
            return BadRequest(ex.Message);
        
        }
    }
    
    [Authorize(Roles = "Admin, User")]
    [HttpPut("/api/devices/{id}")]
    public async Task<IActionResult> UpdateDevice(int id, UpdateDeviceDto dto, CancellationToken ct)
    {
        try
        {
            var currentUserName = User.Identity?.Name;
            if (currentUserName == null)
                return Forbid();
            var currentUser = await _accountService.GetAccountByUsername(currentUserName, ct);
            if (currentUser.Role.Name != "Admin")
            {
                var deviceIdsByEmployee = await _deviceService.GetDeviceIdsByEmployeeId(currentUser.EmployeeId, ct);
                if (!deviceIdsByEmployee.Contains(id))
                    return Forbid();
            }
            
            await _deviceService.UpdateDevice(id, dto, ct);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No device found with id: '{id}'");
        }
        catch (InvalidDeviceTypeException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    [Authorize(Roles = "Admin, User")]
    [HttpDelete("/api/devices/{id}")]
    public async Task<IActionResult> DeleteDevice(int id, CancellationToken ct)
    {
        try
        {
            await _deviceService.DeleteDevice(id, ct);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No device found with id: '{id}'");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    
    
}