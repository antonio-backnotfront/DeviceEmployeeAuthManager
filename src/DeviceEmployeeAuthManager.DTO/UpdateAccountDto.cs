using System.ComponentModel.DataAnnotations;

namespace src.DeviceEmployeeAuthManager.DTO;

public class UpdateAccountDto
{
    public int Id;
    [Required]
    [RegularExpression(@"^[^\d]\w*", ErrorMessage = "Username must not start with a number.")]
    public string Username { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{12,}$",
        ErrorMessage = "Password must be at least 12 characters long and include one lowercase letter, one uppercase letter, one number, and one special character.")]
    public string Password { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public int RoleId { get; set; }
    
}