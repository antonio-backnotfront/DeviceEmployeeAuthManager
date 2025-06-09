using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.DeviceEmployeeAuthManager.Models;

[Table("DeviceType")]
public partial class DeviceType
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}