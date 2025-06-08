using System.Text.Json;

namespace src.DeviceEmployeeAuthManager.DTO;

public class CreateDeviceDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? DeviceType { get; set; }
    public bool IsEnabled { get; set; }
    public JsonElement AdditionalProperties { get; set; }
}