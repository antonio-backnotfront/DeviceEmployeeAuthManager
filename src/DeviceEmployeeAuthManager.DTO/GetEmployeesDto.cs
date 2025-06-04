namespace src.DeviceEmployeeAuthManager.DTO;

public class GetEmployeesDto
{
    public GetEmployeesDto()
    {
    }

    public int Id { get; set; }
    public string FullName { get; set; }

    public GetEmployeesDto(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }
}