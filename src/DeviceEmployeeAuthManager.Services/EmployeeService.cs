using Microsoft.EntityFrameworkCore;
using src.DeviceEmployeeAuthManager.DAL;
// using src.DeviceEmployeeAuthManager.;
using src.DeviceEmployeeAuthManager.Services;
using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Services;

public class EmployeeService : IEmployeeService
{
    private readonly DeviceEmployeeDbContext _context;

    public EmployeeService(DeviceEmployeeDbContext context) =>
        _context = context;

    public async Task<List<GetEmployeesDto>> GetAllEmployees(CancellationToken cancellationToken)
    {
        try
        {
            var employees = await _context.Employees.Include(emp => emp.Person).ToListAsync(cancellationToken);
            return employees.Select(e => new GetEmployeesDto
            {
                Id = e.Id,
                FullName = $"{e.Person.FirstName} {e.Person.MiddleName} {e.Person.LastName}"
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error while getting all employees", ex);
        }
    }

    public async Task<GetEmployeeDto?> GetEmployeeById(int id, CancellationToken cancellationToken)
    {
        // var employee = await _employeeRepository.GetEmployeeById(id, cancellationToken);
        try
        {
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
        catch (Exception ex)
        {
            throw new ApplicationException("Error while getting employee by id", ex);
        }
    }
}