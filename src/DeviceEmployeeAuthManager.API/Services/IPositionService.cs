using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DTO;

namespace DeviceEmployeeAuthManager.API.Services;

public interface IPositionService
{
    public Task<List<GetPositionsDto>> GetAllPositions(CancellationToken cancellationToken);
}