using DeviceEmployeeAuthManager.API.Models;

namespace DeviceEmployeeAuthManager.API.DTO;

public class GetAccountsDto
{
    public GetAccountsDto(Account account)
    {
        Id = account.Id;
        Username = account.Username;
    }

    public int Id { get; set; }
    public string Username { get; set; }
}