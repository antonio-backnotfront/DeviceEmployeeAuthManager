using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeviceEmployeeAuthManager.API.Models;

[Table("Role")]
public class Role
{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }


    public virtual ICollection<Account> Accounts { get; set; }
}