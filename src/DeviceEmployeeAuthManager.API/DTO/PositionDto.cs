namespace DeviceEmployeeAuthManager.API.DTO;

public class PositionDto
{
    public PositionDto(int id, string positionName)
    {
        Id = id;
        PositionName = positionName;
    }

    public PositionDto()
    {
    }

    public int Id { get; set; }
    public string PositionName { get; set; }
}