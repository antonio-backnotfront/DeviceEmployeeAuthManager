using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace src.DeviceEmployeeAuthManager.DTO;

public class UpdateDeviceDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string? DeviceType { get; set; }
    [Required]
    public bool IsEnabled { get; set; }
    [Required]
    public JsonElement AdditionalProperties { get; set; }

    public UpdateDeviceDto(string name, string? deviceType, bool isEnabled, JsonElement additionalProperties)
    {
        Name = name;
        DeviceType = deviceType;
        IsEnabled = isEnabled;
        AdditionalProperties = additionalProperties;
    }
}