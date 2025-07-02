using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DTO;
using DeviceEmployeeAuthManager.API.Exceptions;
using DeviceEmployeeAuthManager.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeviceEmployeeAuthManager.API.Controllers;

[ApiController]
[Route("/api/devices/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IDeviceService _deviceService;
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IDeviceService deviceService, IAccountService accountService,
        ILogger<DeviceController> logger)
    {
        _deviceService = deviceService;
        _accountService = accountService;
        _logger = logger;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("/api/devices/")]
    public async Task<IActionResult> GetDevices(CancellationToken ct)
    {
        try
        {
            var results = await _deviceService.GetAllDevices(ct);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("/api/devices/types")]
    public async Task<IActionResult> GetDeviceTypes(CancellationToken ct)
    {
        try
        {
            var results = await _deviceService.GetDeviceTypesDto(ct);
            return Ok(results);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
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

            var device = await _deviceService.GetDeviceById(id, ct);
            return device is not null ? Ok(device) : NotFound("Device not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
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
            _logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
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
            _logger.LogError("The device that needs to be updated was not found");
            return NotFound($"No device found with id: '{id}'");
        }
        catch (InvalidDeviceTypeException ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }

    [Authorize(Roles = "Admin")]
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
            _logger.LogError("The device that needs to be deleted was not found");
            return NotFound($"No device found with id: '{id}'");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }
}