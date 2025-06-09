using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace src.DeviceEmployeeAuthManager.Models;

[Table("Employee")]
public partial class Employee
{
    [Key]
    public int Id { get; set; }
    public decimal Salary { get; set; }

    // position
    public int PositionId { get; set; }
    [ForeignKey(nameof(PositionId))]
    public Position Position { get; set; }

    // person
    public int PersonId { get; set; }
    [ForeignKey(nameof(PersonId))]
    public Person Person { get; set; }
    
    public DateTime HireDate { get; set; }
    
    public ICollection<DeviceEmployee> DeviceEmployees { get; set; }


}