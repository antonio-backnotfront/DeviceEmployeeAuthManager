namespace DeviceEmployeeAuthManager.API.DTO;

public class GetEmployeesDto
{
    public GetEmployeesDto()
    {
    }

    public GetEmployeesDto(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
}