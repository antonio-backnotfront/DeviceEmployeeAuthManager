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

[Authorize]
[ApiController]
[Route("/api/employee/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<DeviceController> _logger;

    public EmployeeController(IEmployeeService employeeService, IAccountService accountService,
        ILogger<DeviceController> logger)
    {
        _logger = logger;
        _employeeService = employeeService;
        _accountService = accountService;
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("/api/employees")]
    public async Task<IActionResult> GetAllEmployees(CancellationToken ct)
    {
        try
        {
            var currentUserName = User.Identity?.Name;
            if (currentUserName == null)
                return Forbid();
            var currentUser = await _accountService.GetAccountByUsername(currentUserName, ct);
            if (currentUser.Role.Name != "Admin")
                return Forbid();
            var list = await _employeeService.GetAllEmployees(ct);
            return Ok(list);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("/api/employees/{id}")]
    public async Task<IActionResult> GetEmployeeById(int id, CancellationToken ct)
    {
        try
        {
            var currentUserName = User.Identity?.Name;
            if (currentUserName == null)
                return Forbid();
            var currentUser = await _accountService.GetAccountByUsername(currentUserName, ct);
            var employee = await _employeeService.GetEmployeeById(id, ct);

            if (currentUser.Role.Name != "Admin" && currentUser.EmployeeId != id)
                return Forbid();
            return employee != null ? Ok(employee) : NotFound("Employee not found");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Problem();
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/employees/")]
    public async Task<IActionResult> AddEmployee(CreateEmployeeDto dto, CancellationToken ct)
    {
        try
        {
            CreateEmployeeDto response = await _employeeService.CreateEmployee(dto, ct);
            return Created("/api/employees", response);
        }
        catch (InvalidDeviceTypeException ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}