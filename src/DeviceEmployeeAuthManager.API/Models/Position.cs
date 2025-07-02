using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceEmployeeAuthManager.API.Models;

[Table("Position")]
public class Position
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public int MinExpYears { get; set; }


    public virtual ICollection<Employee> Employees { get; set; }
}