using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace src.DeviceEmployeeAuthManager.DTO;

public class CreateDeviceDto
{
    public int? Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int TypeId { get; set; }
    [Required]
    public bool IsEnabled { get; set; }
    [Required]
    public JsonElement AdditionalProperties { get; set; }
}