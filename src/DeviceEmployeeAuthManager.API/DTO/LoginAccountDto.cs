using System.ComponentModel.DataAnnotations;

namespace DeviceEmployeeAuthManager.API.DTO;

public class LoginAccountDto
{
    [Required] public string Login { get; set; }

    [Required] public string Password { get; set; }
}