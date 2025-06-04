using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.DeviceEmployeeAuthManager.DTO;
using src.DeviceEmployeeAuthManager.Services;

namespace src.DeviceEmployeeAuthManager.Controllers;


[ApiController]
[Route("/api/accounts/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService accountService)
    {
        this._service = accountService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("/api/accounts")]
    public async Task<IActionResult> GetAllAccounts(CancellationToken ct)
    {
        try
        {
            var list = await _service.GetAllAccounts(ct);
            return list.Any() ? Ok(list) : NotFound("No accounts found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("/api/accounts/{id}")]
    public async Task<IActionResult> GetAccountById(int id, CancellationToken ct)
    {
        try
        {
            var currentUserName = User.Identity?.Name;
            // Console.WriteLine($"currentUserName: {currentUserName}");
            if (currentUserName == null)
                return Forbid();
            var currentUser = await _service.GetAccountByUsername(currentUserName, ct);
            // if (currentUser == null)
            //     return Forbid();
            // Console.WriteLine($"currentUserId: {currentUser.Id}");
            // Console.WriteLine($"searched usedId: {id}");
            if (currentUser.Role.Name != "Admin" && currentUser.Id != id)
                return Forbid();

            var account = await _service.GetAccountById(id, ct);
            
            return account != null ? Ok(account) : NotFound("Account not found");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("/api/accounts")]
    public async Task<IActionResult> CreateAccount(CreateAccountDto dto, CancellationToken ct)
    {
        try
        {
            var account = await _service.CreateAccount(dto, ct);
            return CreatedAtAction("GetAccountById", new { id = account.Id }, account);
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    [Authorize(Roles = "Admin, User")]
    [HttpPut("/api/accounts/{id}")]
    public async Task<IActionResult> UpdateAccount(int id, UpdateAccountDto dto, CancellationToken ct)
    {
        try
        {
            var currentUserName = User.Identity?.Name;
            if (currentUserName == null)
                return Forbid();
            var currentUser = await _service.GetAccountByUsername(currentUserName, ct);
            if (currentUser.Role.Name != "Admin" && currentUser.Id != id)
                return Forbid();

            await _service.UpdateAccount(id, dto, ct);
            return Ok();
        }
        catch (KeyNotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpDelete("/api/accounts/{id}")]
    public async Task<IActionResult> DeleteAccount(int id, CancellationToken ct)
    {
        try
        {
            await _service.DeleteAccount(id, ct);
            return Ok();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No account found with id: '{id}'");
        }
        catch (Exception ex)
        {
            return Problem(detail: ex.Message);
        }
    }
    
}