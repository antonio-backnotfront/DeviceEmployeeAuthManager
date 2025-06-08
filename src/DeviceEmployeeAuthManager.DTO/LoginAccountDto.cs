using System.ComponentModel.DataAnnotations;

namespace src.DeviceEmployeeAuthManager.DTO;

public class LoginAccountDto
{
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
}