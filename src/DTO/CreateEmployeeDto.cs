using System.ComponentModel.DataAnnotations;

namespace src.DeviceEmployeeAuthManager.DTO;

public class CreateEmployeeDto
{
    public int? Id { get; set; }
    [Required]
    public CreatePersonDto Person { get; set; }
    [Required]
    public int PositionId {get;set;}
    [Required]
    [Range(0.001, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
    public decimal Salary {get;set;}
    public DateTime HireDate { get; set; } = DateTime.Now;
}