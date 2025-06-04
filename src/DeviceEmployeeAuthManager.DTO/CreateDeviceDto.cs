using System.Text.Json;

namespace src.DeviceEmployeeAuthManager.DTO;

public class CreateDeviceDto
{
    public CreateDeviceDto(string name, string? deviceType, bool isEnabled, JsonElement additionalProperties)
    {
        Name = name;
        DeviceType = deviceType;
        IsEnabled = isEnabled;
        AdditionalProperties = additionalProperties;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string? DeviceType { get; set; }
    public bool IsEnabled { get; set; }
    public JsonElement AdditionalProperties { get; set; }
}