using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceEmployeeAuthManager.API.Models;

[Table("Person")]
public class Person
{
    [Key] public int Id { get; set; }

    public string PassportNumber { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;

    public Employee Employee { get; set; } = null!;
}