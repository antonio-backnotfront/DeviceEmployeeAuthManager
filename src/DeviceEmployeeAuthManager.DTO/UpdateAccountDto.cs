using System.ComponentModel.DataAnnotations;

namespace src.DeviceEmployeeAuthManager.DTO;

public class CreateAccountDto
{
    public int Id;
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int RoleId { get; set; }
    
}