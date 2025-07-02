using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DTO;
using DeviceEmployeeAuthManager.API.Models;

namespace DeviceEmployeeAuthManager.API.Services;

public interface IAccountService
{
    public Task<List<GetAccountsDto>> GetAllAccounts(CancellationToken cancellationToken);
    public Task<GetAccountDto?> GetAccountById(int id, CancellationToken cancellationToken);
    public Task<Account> CreateAccount(CreateAccountDto dto, CancellationToken cancellationToken);
    public Task<bool> UpdateAccount(int id, UpdateAccountDto dto, CancellationToken cancellationToken);
    public Task<bool> DeleteAccount(int id, CancellationToken cancellationToken);
    public Task<Account> GetAccountByUsername(string username, CancellationToken cancellationToken);
}