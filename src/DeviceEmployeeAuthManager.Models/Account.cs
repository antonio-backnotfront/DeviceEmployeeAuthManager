using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.DeviceEmployeeAuthManager.Models;

[Table("Account")]

public class Account
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [RegularExpression(@"^[^\d]\w*", ErrorMessage = "Username must not start with a number.")]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{12,}$",
        ErrorMessage = "Password must be at least 12 characters long and include one lowercase letter, one uppercase letter, one number, and one special character.")]
    public string Password { get; set; }
    
    // Employee
    [Required]
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    [Required]
    public Employee Employee { get; set; }
    
    // Role
    [Required]
    public int RoleId { get; set; }
    [ForeignKey(nameof(RoleId))]
    [Required]
    public Role Role { get; set; }
    
    
}