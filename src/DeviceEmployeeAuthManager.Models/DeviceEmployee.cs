using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.DeviceEmployeeAuthManager.Models;

[Table("DeviceEmployee")]
public partial class DeviceEmployee
{
    [Key]
    public int Id { get; set; }


    [Required]
    public DateTime IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    // device
    [Required]
    public int DeviceId { get; set; }
    [ForeignKey(nameof(DeviceId))]
    public virtual Device Device { get; set; } = null!;

    // employee
    [Required]
    public int EmployeeId { get; set; }
    [ForeignKey(nameof(EmployeeId))]
    public virtual Employee Employee { get; set; } = null!;
}