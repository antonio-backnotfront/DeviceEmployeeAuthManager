using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceEmployeeAuthManager.API.Models;

[Table("DeviceEmployee")]
public class DeviceEmployee
{
    [Key] public int Id { get; set; }


    public DateTime IssueDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    // device
    public int DeviceId { get; set; }

    [ForeignKey(nameof(DeviceId))] public virtual Device Device { get; set; } = null!;

    // employee
    public int EmployeeId { get; set; }

    [ForeignKey(nameof(EmployeeId))] public virtual Employee Employee { get; set; } = null!;
}