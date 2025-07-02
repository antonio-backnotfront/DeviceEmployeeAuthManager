using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceEmployeeAuthManager.API.Models;

[Table("Account")]
public class Account
{
    [Key] public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    // Employee
    public int EmployeeId { get; set; }

    [ForeignKey(nameof(EmployeeId))] public Employee Employee { get; set; }

    // Role
    public int RoleId { get; set; }

    [ForeignKey(nameof(RoleId))] public Role Role { get; set; }
}