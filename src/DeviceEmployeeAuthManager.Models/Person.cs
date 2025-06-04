using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.DeviceEmployeeAuthManager.Models;

[Table("Person")]
public partial class Person
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string PassportNumber { get; set; } = null!;

    [Required]
    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    public Employee Employee { get; set; } = null!;

}