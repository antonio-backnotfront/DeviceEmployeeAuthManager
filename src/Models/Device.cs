using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.DeviceEmployeeAuthManager.Models;

[Table("Device")]
public class Device
{

    [Key]
    public int Id {get; set;}

    public string Name {get; set;}
    
    public bool IsEnabled { get; set; }
    
    public string AdditionalProperties { get; set; }
    
    public int? DeviceTypeId { get; set; }
    [ForeignKey(nameof(DeviceTypeId))]
    public virtual DeviceType? DeviceType { get; set; }

    public virtual ICollection<DeviceEmployee> DeviceEmployees { get; set; } = new List<DeviceEmployee>();

}