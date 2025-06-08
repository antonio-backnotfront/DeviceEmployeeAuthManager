using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace src.DeviceEmployeeAuthManager.DTO;

public class UpdateDeviceDto
{
    public string Name { get; set; }
    public string? DeviceType { get; set; }
    public string IsEnabled { get; set; }
    public JsonElement AdditionalProperties { get; set; }

    public UpdateDeviceDto(string name, string? deviceType, string isEnabled, JsonElement additionalProperties)
    {
        Name = name;
        DeviceType = deviceType;
        IsEnabled = isEnabled;
        AdditionalProperties = additionalProperties;
    }
}