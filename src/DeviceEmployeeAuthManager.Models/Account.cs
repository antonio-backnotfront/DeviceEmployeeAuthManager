using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.DeviceEmployeeAuthManager.Models;

[Table("Account")]
public class Account
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
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