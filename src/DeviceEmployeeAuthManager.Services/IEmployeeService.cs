using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Repositories;

public interface IEmployeeService
{
    public Task<List<GetEmployeesDto>> GetAllEmployees(CancellationToken cancellationToken);
    public Task<GetEmployeeDto?> GetEmployeeById(int id, CancellationToken cancellationToken);
}