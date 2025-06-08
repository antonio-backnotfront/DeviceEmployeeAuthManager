using System.ComponentModel.DataAnnotations;

namespace src.DeviceEmployeeAuthManager.DTO;

public class GetDevicesDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public GetDevicesDto()
    { }

    public GetDevicesDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}