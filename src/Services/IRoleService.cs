using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Services;

public interface IRoleService
{
    public Task<List<GetRoleDto>> GetAllRoles(CancellationToken cancellationToken);
}