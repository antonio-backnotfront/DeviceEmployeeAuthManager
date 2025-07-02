using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DTO;

namespace DeviceEmployeeAuthManager.API.Services;

public interface IEmployeeService
{
    public Task<List<GetEmployeesDto>> GetAllEmployees(CancellationToken cancellationToken);
    public Task<GetEmployeeDto?> GetEmployeeById(int id, CancellationToken cancellationToken);
    public Task<CreateEmployeeDto> CreateEmployee(CreateEmployeeDto dto, CancellationToken cancellationToken);
}