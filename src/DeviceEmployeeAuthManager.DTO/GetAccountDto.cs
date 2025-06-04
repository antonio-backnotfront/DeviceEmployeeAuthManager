using src.DeviceEmployeeAuthManager.Models;

namespace src.DeviceEmployeeAuthManager.DTO;

public class GetAccountDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public GetAccountDto(Account account)
    {
        Username = account.Username;
        Password = account.Password;
    }
}