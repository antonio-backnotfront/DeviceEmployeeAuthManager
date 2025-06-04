namespace src.DeviceEmployeeAuthManager.DTO;

public class GetEmployeeDto
{
    public GetEmployeeDto(PersonDto personDto, decimal salary, object positionDto, DateTime hireDate)
    {
        PersonDto = personDto;
        Salary = salary;
        PositionDto = positionDto;
        HireDate = hireDate;
    }

    public GetEmployeeDto()
    {
    }

    public PersonDto PersonDto { get; set; }
    public object PositionDto { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}