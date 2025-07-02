using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DTO;

namespace DeviceEmployeeAuthManager.API.Services;

public interface IRoleService
{
    public Task<List<GetRoleDto>> GetAllRoles(CancellationToken cancellationToken);
}