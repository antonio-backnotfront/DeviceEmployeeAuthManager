namespace DeviceEmployeeAuthManager.API.DTO;

public class GetDevicesDto
{
    public GetDevicesDto()
    {
    }

    public GetDevicesDto(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
}