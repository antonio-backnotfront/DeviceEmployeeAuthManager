using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using src.DeviceEmployeeAuthManager.DAL;
using src.DeviceEmployeeAuthManager.DTO;
using src.DeviceEmployeeAuthManager.Models;

namespace src.DeviceEmployeeAuthManager.Services;

public class AccountService : IAccountService
{
    private readonly DeviceEmployeeDbContext _context;
    private readonly PasswordHasher<Account> _passwordHasher = new PasswordHasher<Account>();

    public AccountService(DeviceEmployeeDbContext context)
    {
        _context = context;
    }
    public async Task<List<GetAccountsDto>> GetAllAccounts(CancellationToken cancellationToken)
    {
        try
        {
            var accounts = await _context.Accounts
                .ToListAsync(cancellationToken);
            return accounts.Select(acc => new GetAccountsDto(acc)).ToList();
        } catch (Exception ex)
        {
            throw new ApplicationException("Error getting accounts", ex);
        }
    }

    public async Task<GetAccountDto?> GetAccountById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(acc => acc.Id == id, cancellationToken);
            if (acc == null) return null;
            string roleName =
                (await _context.Roles.FirstOrDefaultAsync(role => role.Id == acc.RoleId, cancellationToken)).Name;
            GetAccountDto dto = new GetAccountDto();
            dto.Username = acc.Username;
            dto.Role = roleName;
            return dto;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error getting account", ex);
        }
    }
    
    public async Task<Account> CreateAccount(CreateAccountDto dto, CancellationToken cancellationToken)
    {
        

        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == dto.EmployeeId, cancellationToken)
                         ?? throw new KeyNotFoundException("Employee not found");
        
        var role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == dto.RoleId, cancellationToken)
                         ?? throw new KeyNotFoundException("Role not found");
        
        var account = new Account
        {
            Username = dto.Username,
            Password = dto.Password,
            EmployeeId = dto.EmployeeId,
            RoleId = dto.RoleId
        };
        account.Password = _passwordHasher.HashPassword(account, dto.Password);
        
        await _context.Accounts.AddAsync(account, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        dto.Id = account.Id;
        return account;
    }
    
    public async Task<bool> UpdateAccount(int id, UpdateAccountDto dto, CancellationToken cancellationToken)
    {
        
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == dto.EmployeeId, cancellationToken)
                         ?? throw new KeyNotFoundException("Employee not found");
        
        var account = _context.Accounts.FirstOrDefault(e => e.Id == id);
        if (account == null) throw new KeyNotFoundException("Account not found");
        var role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == dto.RoleId, cancellationToken)
                         ?? throw new KeyNotFoundException("Role not found");
        
        account.Username = dto.Username;
        account.Password = _passwordHasher.HashPassword(account, dto.Password);
        account.EmployeeId = dto.EmployeeId;
        account.RoleId = dto.RoleId;
        
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
    
    public async Task<bool> DeleteAccount(int id, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts.FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        if (account == null)
            throw new KeyNotFoundException($"Device with ID {id} not found.");
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Account> GetAccountByUsername(string username, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .Include(a => a.Role)
            .FirstOrDefaultAsync(acc => acc.Username == username, cancellationToken);
        if (account == null) throw new KeyNotFoundException($"Account with username {username} not found.");
        return account;
    }
}