using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeviceEmployeeAuthManager.API.Controllers;

using DeviceEmployeeAuthManager.API.DAL;
using DeviceEmployeeAuthManager.API.DTO;
using DeviceEmployeeAuthManager.API.Models;
using DeviceEmployeeAuthManager.API.Services.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/auth/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DeviceEmployeeDbContext _context;
    private readonly ILogger<AuthController> _logger;
    private readonly PasswordHasher<Account> _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthController(DeviceEmployeeDbContext context, ITokenService tokenService, ILogger<AuthController> logger)
    {
        _logger = logger;
        _tokenService = tokenService;
        _context = context;
        _passwordHasher = new PasswordHasher<Account>();
    }

    [HttpPost("/api/auth")]
    public async Task<IActionResult> Auth(LoginAccountDto dto, CancellationToken ct)
    {
        try
        {
            var foundUser = await _context.Accounts
                .Include(e => e.Role)
                .FirstOrDefaultAsync(e => e.Username == dto.Login, ct);
            if (foundUser == null)
            {
                _logger.LogError("The user wasn't found");
                return Unauthorized();
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(foundUser, foundUser.Password, dto.Password);
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                _logger.LogError("The user password is incorrect");
                return Unauthorized();
            }

            var tokens = new
            {
                AccessToken = _tokenService.GenerateToken(foundUser.Username, foundUser.Role.Name)
            };
            return Ok(tokens);
        }
        catch (KeyNotFoundException)
        {
            _logger.LogError("Something went wrong during Authentication");
            return Unauthorized();
        }
    }
}