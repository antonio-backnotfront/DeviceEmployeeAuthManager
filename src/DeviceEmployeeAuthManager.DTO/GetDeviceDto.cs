using System.Text.Json;

namespace src.DeviceEmployeeAuthManager.DTO;

public class GetDeviceDto
{
    public string Name { get; set; }
    public bool IsEnabled { get; set; }
    public string Type { get; set; }
    public object AdditionalProperties { get; set; }
    
}