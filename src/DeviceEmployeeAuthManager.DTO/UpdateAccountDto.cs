using System.ComponentModel.DataAnnotations;

namespace src.DeviceEmployeeAuthManager.DTO;

public class UpdateAccountDto
{
    public int Id;
    public string Username { get; set; }
    public string Password { get; set; }
    public int EmployeeId { get; set; }
    public int RoleId { get; set; }
    
}