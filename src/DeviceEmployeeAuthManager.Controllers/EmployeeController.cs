using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DeviceEmployeeAuthManager.Services;

namespace src.DeviceEmployeeAuthManager.Controllers;

[Authorize]
[ApiController]
[Route("/api/employee/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService employeeService)
    {
        this._service = employeeService;
    }

    [HttpGet("/api/employee")]
    public async Task<IActionResult> GetAllEmployees(CancellationToken ct)
    {
        try
        {
            var list = await _service.GetAllEmployees(ct);
            return list.Any() ? Ok(list) : NotFound("No employees found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }

    [HttpGet("/api/employee/{id}")]
    public async Task<IActionResult> GetEmployeeById(int id, CancellationToken ct)
    {
        try
        {
            var employee = await _service.GetEmployeeById(id, ct);
            return employee != null ? Ok(employee) : NotFound("Employee not found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
}