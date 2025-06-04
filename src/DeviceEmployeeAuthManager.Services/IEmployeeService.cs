using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Services;

public interface IEmployeeService
{
    public Task<List<GetEmployeesDto>> GetAllEmployees(CancellationToken cancellationToken);
    public Task<GetEmployeeDto?> GetEmployeeById(int id, CancellationToken cancellationToken);
}