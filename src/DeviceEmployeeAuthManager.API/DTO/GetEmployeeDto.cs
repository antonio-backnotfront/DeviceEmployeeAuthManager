using System;

namespace DeviceEmployeeAuthManager.API.DTO;

public class GetEmployeeDto
{
    public GetEmployeeDto(PersonDto personDto, decimal salary, string position, DateTime hireDate)
    {
        Person = personDto;
        Salary = salary;
        Position = position;
        HireDate = hireDate;
    }

    public GetEmployeeDto()
    {
    }

    public PersonDto Person { get; set; }
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}