using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.DeviceEmployeeAuthManager.DAL;
// using src.DeviceEmployeeAuthManager.;
using src.DeviceEmployeeAuthManager.Services;
using src.DeviceEmployeeAuthManager.DTO;
using src.DeviceEmployeeAuthManager.Exceptions;
using src.DeviceEmployeeAuthManager.Models;

namespace src.DeviceEmployeeAuthManager.Services;

public class EmployeeService : IEmployeeService
{
    private readonly DeviceEmployeeDbContext _context;

    public EmployeeService(DeviceEmployeeDbContext context) =>
        _context = context;

    public async Task<List<GetEmployeesDto>> GetAllEmployees(CancellationToken cancellationToken)
    {
        var employees = await _context.Employees.Include(emp => emp.Person).ToListAsync(cancellationToken);
        return employees.Select(e => new GetEmployeesDto
        {
            Id = e.Id,
            FullName = $"{e.Person.FirstName} {e.Person.MiddleName} {e.Person.LastName}"
        }).ToList();
    }

    public async Task<GetEmployeeDto?> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        // var employee = await _employeeRepository.GetEmployeeById(id, cancellationToken);

        var employee = await _context
            .Employees
            .Include(emp => emp.Person)
            .Include(emp => emp.Position)
            .FirstOrDefaultAsync(emp => emp.Id == id, cancellationToken);
        if (employee is null) return null;

        return new GetEmployeeDto
        {
            Person = new PersonDto
            {
                Id = employee.Id,
                FirstName = employee.Person.FirstName,
                MiddleName = employee.Person.MiddleName,
                LastName = employee.Person.LastName,
                Email = employee.Person.Email,
                PassportNumber = employee.Person.PassportNumber,
                PhoneNumber = employee.Person.PhoneNumber
            },
            Position = employee.Position.Name,
            HireDate = employee.HireDate,
            Salary = employee.Salary
        };
    }

    public async Task<CreateEmployeeDto> CreateEmployee(CreateEmployeeDto dto, CancellationToken cancellationToken)
    {
        var position = await _context.Positions.FirstOrDefaultAsync(e => e.Id == dto.PositionId);
        if (position == null)
        {
            throw new KeyNotFoundException("Position not found");
        }

        CreatePersonDto personDto = new CreatePersonDto()
        {
            FirstName = dto.Person.FirstName,
            MiddleName = dto.Person.MiddleName,
            LastName = dto.Person.LastName,
            Email = dto.Person.Email,
            PassportNumber = dto.Person.PassportNumber,
            PhoneNumber = dto.Person.PhoneNumber
        };
        Person person = new Person
        {
            PassportNumber = personDto.PassportNumber,
            FirstName = personDto.FirstName,
            MiddleName = personDto.MiddleName,
            LastName = personDto.LastName,
            Email = personDto.Email,
            PhoneNumber = personDto.PhoneNumber
        };
        await _context.Persons.AddAsync(person, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        Employee employee = new Employee()
        {
            PositionId = dto.PositionId,
            Salary = dto.Salary,
            HireDate = dto.HireDate,
            PersonId = person.Id,
        };
        await _context.Employees.AddAsync(employee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        dto.Id = employee.Id;
        // var device = new Device
        // {
        //     Name = dto.Name,
        //     DeviceType = deviceType,
        //     IsEnabled = bool.Parse(dto.IsEnabled),
        //     AdditionalProperties = dto.AdditionalProperties.GetRawText()
        // };
        // await _context.Devices.AddAsync(device, cancellationToken);
        // dto.Id = device.Id;
        return dto;
    }
}