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
    private readonly IDeviceService service;

    public DeviceController(IDeviceService deviceService)
    {
        this.service = deviceService;
    }

    [HttpGet("/api/devices/")]
    public async Task<IActionResult> GetDevices(CancellationToken ct)
    {
        try
        {
            var results = await service.GetAllDevices(ct);
            return results.Count > 0 ? Ok(results) : NotFound("No devices found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    [HttpGet("/api/devices/{id}")]
    public async Task<IActionResult> GetDevice(int id, CancellationToken ct)
    {
        try
        {
            var device = await service.GetDeviceById(id,ct);
            return device is not null ? Ok(device) : NotFound("Device not found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    [HttpPost("/api/devices/")]
    public async Task<IActionResult> AddDevice(CreateDeviceDto dto, CancellationToken ct)
    {
        try
        {
            await service.CreateDevice(dto, ct);
            return Created("/api/devices", dto);
        }
        catch (InvalidDeviceTypeException ex)
        {
            return BadRequest(ex.Message);
        
        }
    }
    [HttpPut("/api/devices/{id}")]
    public async Task<IActionResult> UpdateDevice(int id, UpdateDeviceDto dto, CancellationToken ct)
    {
        try
        {
            await service.UpdateDevice(id, dto, ct);
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
    
    [HttpDelete("/api/devices/{id}")]
    public async Task<IActionResult> DeleteDevice(int id, CancellationToken ct)
    {
        try
        {
            await service.DeleteDevice(id, ct);
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