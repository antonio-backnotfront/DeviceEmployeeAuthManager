using src.DeviceEmployeeAuthManager.Models;

namespace src.DeviceEmployeeAuthManager.DTO;

public class GetAccountsDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public GetAccountsDto(Account account)
    {
        Id = account.Id;
        Username = account.Username;
        Password = account.Password;
    }
}